using Cinemachine;
using Dreyever;
using System.Collections;
using UnityEngine;

public class VirtualCameraManagement : MonoBehaviour {

    private CinemachineBrain cinemachineBrain;
    private float blendDuration;

    private Transform playerTransform;
    private State state;

    public GameObject virtualCameraRight;
    public GameObject virtualCameraLeft;

    void Start()
    {
        cinemachineBrain = GetComponent<CinemachineBrain>();
        blendDuration = cinemachineBrain.m_DefaultBlend.m_Time;

        playerTransform = GameObject.FindGameObjectWithTag("dreyever").transform;
        state = playerTransform.GetComponent<State>();

        StartFollowing();
    }

    void Update () {
		if(state.GetDirection() == Direction.RIGHT)
        {
            virtualCameraRight.SetActive(true);
            virtualCameraLeft.SetActive(false);
        } else
        {
            virtualCameraRight.SetActive(false);
            virtualCameraLeft.SetActive(true);
        }
	}

    public void StartFollowing()
    {
        
        GameObject currentVirtualCamera = virtualCameraLeft.activeSelf ? virtualCameraLeft : virtualCameraRight;
        GameObject newVirtualCamera = Instantiate(currentVirtualCamera);
        newVirtualCamera.SetActive(false);
        newVirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerTransform;
        
        if(virtualCameraLeft.activeSelf)
        {
            virtualCameraLeft = newVirtualCamera;
            virtualCameraRight.GetComponent<CinemachineVirtualCamera>().Follow = playerTransform;
        } else
        {
            virtualCameraRight = newVirtualCamera;
            virtualCameraLeft.GetComponent<CinemachineVirtualCamera>().Follow = playerTransform;
        }

        newVirtualCamera.SetActive(true);
        currentVirtualCamera.SetActive(false);

        StartCoroutine(DestroyVirtualCamera(currentVirtualCamera));
    }

    public void StopFollowing()
    {
        virtualCameraRight.GetComponent<CinemachineVirtualCamera>().Follow = null;
        virtualCameraLeft.GetComponent<CinemachineVirtualCamera>().Follow = null;
    }

    private IEnumerator DestroyVirtualCamera(GameObject virtualCamera)
    {
        // Adding the 1 just for certainty.
        yield return new WaitForSeconds(blendDuration + 1);
        Destroy(virtualCamera);
    }
}

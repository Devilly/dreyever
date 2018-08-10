using Cinemachine;
using Dreyever;
using UnityEngine;

public class VirtualCameraManagement : MonoBehaviour {

    private Transform playerTransform;
    private State state;

    public GameObject virtualCameraRight;
    public GameObject virtualCameraLeft;

    void Start()
    {
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

        Destroy(currentVirtualCamera);
    }

    public void StopFollowing()
    {
        virtualCameraRight.GetComponent<CinemachineVirtualCamera>().Follow = null;
        virtualCameraLeft.GetComponent<CinemachineVirtualCamera>().Follow = null;
    }
}

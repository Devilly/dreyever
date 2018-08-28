using Cinemachine;
using Dreyever;
using System.Collections;
using UnityEngine;

public class VirtualCameraManagement : MonoBehaviour {

    public float[] cameraHeights;
    private float currentCameraHeight;

    private CinemachineBrain cinemachineBrain;
    private float blendDuration;

    private Transform playerTransform;
    private State state;

    public GameObject virtualCameraRight;
    public GameObject virtualCameraLeft;

    private LevelBoundsInfo levelBoundsInfo;

    void Start()
    {
        cinemachineBrain = GetComponent<CinemachineBrain>();
        blendDuration = cinemachineBrain.m_DefaultBlend.m_Time;

        playerTransform = GameObject.FindGameObjectWithTag("dreyever").transform;
        state = playerTransform.GetComponent<State>();

        levelBoundsInfo = Camera.main.GetComponent<LevelBoundsInfo>();

        currentCameraHeight = FindNearestCameraHeight();

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

        //float nearestCameraHeight = FindNearestCameraHeight();
        //if(!Mathf.Approximately(nearestCameraHeight, currentCameraHeight))
        //{
        //    currentCameraHeight = nearestCameraHeight;

        //    StopFollowing();
        //    StartFollowing();
        //}
	}

    private float FindNearestCameraHeight()
    {
        int bestIndex = 0;
        float bestDifference = Mathf.Abs(playerTransform.position.y - cameraHeights[0]);

        for (int index = 1; index < cameraHeights.Length; index++)
        {
            float difference = Mathf.Abs(playerTransform.position.y - cameraHeights[index]);
            if(difference < bestDifference)
            {
                bestDifference = difference;
                bestIndex = index;
            }
        }

        return cameraHeights[bestIndex];
    }

    public void StartFollowing()
    {
        GameObject currentVirtualCamera;
        if (virtualCameraLeft.activeSelf)
        {
            currentVirtualCamera = virtualCameraLeft;

            virtualCameraRight.GetComponent<FollowAlongHorizontally>().yPosition = currentCameraHeight;
            virtualCameraRight.GetComponent<FollowAlongWithinBounds>().bounds = levelBoundsInfo.totalBounds;
        } else
        {
            currentVirtualCamera = virtualCameraRight;

            virtualCameraLeft.GetComponent<FollowAlongHorizontally>().yPosition = currentCameraHeight;
            virtualCameraLeft.GetComponent<FollowAlongWithinBounds>().bounds = levelBoundsInfo.totalBounds;
        }

        GameObject newVirtualCamera = Instantiate(currentVirtualCamera);
        newVirtualCamera.SetActive(false);
        newVirtualCamera.GetComponent<FollowAlongHorizontally>().yPosition = currentCameraHeight;
        newVirtualCamera.GetComponent<FollowAlongWithinBounds>().bounds = levelBoundsInfo.totalBounds;
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

using Dreyever;
using System.Collections;
using System.Collections.Generic;
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
        newVirtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = playerTransform;
        
        if(virtualCameraLeft.activeSelf)
        {
            virtualCameraLeft = newVirtualCamera;
            virtualCameraRight.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = playerTransform;
        } else
        {
            virtualCameraRight = newVirtualCamera;
            virtualCameraLeft.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = playerTransform;
        }

        newVirtualCamera.SetActive(true);
        currentVirtualCamera.SetActive(false);

        Destroy(currentVirtualCamera);
    }

    public void StopFollowing()
    {
        virtualCameraRight.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = null;
        virtualCameraLeft.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = null;
    }
}

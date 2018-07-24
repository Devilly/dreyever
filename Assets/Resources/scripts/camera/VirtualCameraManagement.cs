using Dreyever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraManagement : MonoBehaviour {

    private State state;

    public GameObject virtualCameraRight;
    public GameObject virtualCameraLeft;

    void Start()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        state = playerTransform.Find("Monocar").GetComponent<State>();

        virtualCameraRight.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = playerTransform;
        virtualCameraLeft.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = playerTransform;
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
}

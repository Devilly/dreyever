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
        state = GameObject.FindGameObjectWithTag("Player").transform.Find("Monocar").GetComponent<State>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFail : MonoBehaviour {

    public GameObject dreyever;

    private void FixedUpdate()
    {
        if(dreyever.transform.position.y < -15)
        {
            Time.timeScale = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

    private Transform playerTransform;
    private Transform barrelTransform;
	
	void Start () {
        playerTransform = GameObject.Find("Dreyever").transform;
        barrelTransform = transform;
	}
	
	void FixedUpdate () {
        Vector3 position = transform.position;
        Vector3 playerPosition = playerTransform.position;

        barrelTransform.rotation = Quaternion.FromToRotation(Vector3.down,
            new Vector3(playerPosition.x - position.x, playerPosition.y - position.y, 0));
	}
}

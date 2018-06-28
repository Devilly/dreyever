using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Cannon
{
    public class Aim : MonoBehaviour
    {

        private Transform playerTransform;
        private Transform barrelTransform;

        void Start()
        {
            playerTransform = GameObject.Find("Dreyever").transform;
            barrelTransform = transform;
        }

        void FixedUpdate()
        {
            Vector3 position = transform.position;
            Vector3 playerPosition = playerTransform.position;

            barrelTransform.rotation = Quaternion.FromToRotation(Vector3.down,
                new Vector3(playerPosition.x - position.x, Mathf.Clamp(playerPosition.y - position.y, Mathf.NegativeInfinity, 0)));
        }
    }
}
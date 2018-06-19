using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Cannon
{
    public class Follow : MonoBehaviour
    {
        private Transform playerTransform;

        public float minimumPosition;
        public float maximumPosition;

        void Start()
        {
            playerTransform = GameObject.Find("Dreyever").transform;
        }

        void Update()
        {
           transform.localPosition = new Vector3(Mathf.Clamp(transform.parent.InverseTransformPoint(playerTransform.position).x, minimumPosition, maximumPosition), transform.localPosition.y, transform.localPosition.z);
        }
    }
}
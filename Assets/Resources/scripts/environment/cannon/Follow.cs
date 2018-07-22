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

        public float speed;

        void Start()
        {
            playerTransform = GameObject.Find("Dreyever").transform;
        }

        void FixedUpdate()
        {
            float playerPosition = transform.parent.InverseTransformPoint(playerTransform.position).x;
            float speedClampedNewPosition = Mathf.Clamp(playerPosition, transform.localPosition.x - speed * Time.fixedDeltaTime, transform.localPosition.x + speed * Time.fixedDeltaTime);
            float railsClampedNewPostition = Mathf.Clamp(speedClampedNewPosition, minimumPosition, maximumPosition);
            transform.localPosition = new Vector3(railsClampedNewPostition, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
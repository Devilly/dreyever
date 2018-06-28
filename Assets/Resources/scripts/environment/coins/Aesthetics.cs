using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Coins
{
    public class Aesthetics : MonoBehaviour
    {
        private Animator animator;

        private float defaultPositionY;
        private float sineTime;
        private float previousSineTime;

        void Start()
        {
            animator = GetComponent<Animator>();

            defaultPositionY = transform.position.y;
            sineTime = Mathf.PI;
            previousSineTime = Mathf.PI;
        }

        void Update()
        {
            sineTime += 4 * Time.deltaTime;
            sineTime = sineTime % (2 * Mathf.PI);

            float valueToBling = .3f * Mathf.PI;
            if (previousSineTime < valueToBling  && sineTime > valueToBling)
            {
                animator.Play("bling");
            }

            transform.position = new Vector3(transform.position.x,
                defaultPositionY + Mathf.Sin(sineTime) * .25f,
                transform.position.z);
        }

        void LateUpdate()
        {
            previousSineTime = sineTime;
        }
    }
}
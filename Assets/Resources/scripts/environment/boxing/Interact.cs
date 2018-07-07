using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dead;
using Scriptables.Util;

namespace Environment.Boxing
{
    public class Interact : MonoBehaviour
    {
        public Instantiater instantiater;

        private Transform playerTransform;

        private const float lengthOfExpansion = 1.8f;
        private const float timeToExpand = .05f;
        private const float timeToReset = 2f;

        private float intermediateStartTime;
        private Vector2 startPosition;
        private Vector2 endPosition;

        private Status status = Status.IDLE;
        private enum Status
        {
            IDLE, INTERMEDIATE_STATE, PUNCH, RESET
        }

        void Start()
        {
            playerTransform = GameObject.Find("Dreyever").transform;

            startPosition = transform.position;
            endPosition = startPosition + new Vector2(-lengthOfExpansion, 0);
        }

        void FixedUpdate()
        {
            if(status == Status.IDLE)
            {
                if(transform.position.x - lengthOfExpansion - 2 < playerTransform.position.x &&
                    transform.position.x > playerTransform.position.x)
                {
                    status = Status.PUNCH;
                    intermediateStartTime = Time.time;
                }
            } else if(status == Status.PUNCH)
            {
                float passedTimeFraction = (Time.time - intermediateStartTime) / timeToExpand;
                transform.position = Vector2.Lerp(startPosition, endPosition, passedTimeFraction);

                if(passedTimeFraction >= 1)
                {
                    status = Status.INTERMEDIATE_STATE;
                    StartCoroutine(StartReset());
                }
            } else if(status == Status.RESET)
            {
                float passedTimeFraction = (Time.time - intermediateStartTime) / timeToReset;
                transform.position = Vector2.Lerp(endPosition, startPosition, passedTimeFraction);

                if (passedTimeFraction >= 1)
                {
                    status = Status.IDLE;
                }
            }
        }

        private IEnumerator StartReset()
        {
            yield return new WaitForSeconds(1f);
            status = Status.RESET;
            intermediateStartTime = Time.time;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.name == "Hitbox")
            {
                instantiater.Clone();
            }
        }
    }
}
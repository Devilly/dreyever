using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dead;
using Scriptables.Util;

namespace Rush
{
    public class FallFail : MonoBehaviour
    {
        private Transform playerTransform;
        public Instantiater instantiater;

        void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("dreyever").transform;
        }

        void FixedUpdate()
        {
            if (playerTransform.transform.position.y < -15)
            {
                instantiater.Clone();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dead;
using Scriptables.Util;

namespace Rush
{
    public class FallFail : MonoBehaviour
    {
        private LevelBoundsInfo levelBoundsInfo;
        private float failLevel;

        private Transform playerTransform;
        public Instantiater instantiater;

        void Start()
        {
            levelBoundsInfo = Camera.main.GetComponent<LevelBoundsInfo>();
            failLevel = levelBoundsInfo.environmentalBounds.min.y - 5;

            playerTransform = GameObject.FindGameObjectWithTag("dreyever").transform;
        }

        void FixedUpdate()
        {
            if (playerTransform.transform.position.y < failLevel)
            {
                instantiater.Clone();
            }
        }
    }
}
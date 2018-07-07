using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dead;
using Scriptables.Util;

namespace Rush
{
    public class FallFail : MonoBehaviour
    {
        public GameObject dreyever;
        public Instantiater instantiater;

        private void FixedUpdate()
        {
            if (dreyever.transform.position.y < -15)
            {
                instantiater.Clone();
            }
        }
    }
}
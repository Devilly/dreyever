using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dead;

namespace Rush
{
    public class FallFail : MonoBehaviour
    {

        public GameObject dreyever;
        public Behavior shinigamiBehavior;

        private void FixedUpdate()
        {
            if (dreyever.transform.position.y < -15)
            {
                shinigamiBehavior.Activate();
            }
        }
    }
}
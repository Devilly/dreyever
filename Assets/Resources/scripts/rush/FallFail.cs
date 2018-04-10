using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                shinigamiBehavior.activate();
            }
        }
    }
}
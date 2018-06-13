using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Speed
{
    public class SpeedSectionBehaviour : MonoBehaviour
    {
        public SpeedPartInteract[] speedPartInteracts;

        public float speedIncrease = 2f;

        void Start()
        {
            foreach (SpeedPartInteract speedPartInteract in speedPartInteracts)
            {
                speedPartInteract.SetSpeedIncrease(speedIncrease);
            }
        }
    }
}
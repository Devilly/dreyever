using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreyever
{
    public class SetDreyever : MonoBehaviour
    {
        void Start()
        {
            GetComponent<SpriteRenderer>().sprite = Persistent.Environment.instance.GetCurrentDreyever().tilt[0];
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class MakeNotDestroyed : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
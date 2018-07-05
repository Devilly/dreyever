using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Persistent
{
    public class MakeNotDestroyedSingleton : MonoBehaviour
    {

        private static MakeNotDestroyedSingleton instance = null;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
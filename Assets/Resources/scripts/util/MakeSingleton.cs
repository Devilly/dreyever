using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class MakeSingleton : MonoBehaviour
    {
        private static List<string> instances = new List<string>();

        static MakeSingleton() {
            SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
            {
                instances.Clear();
            };
        }

        void Awake()
        {
            if (instances.Contains(name))
            {
                Destroy(gameObject);
            }
            else
            {
                instances.Add(name);
            }
        }
    }
}
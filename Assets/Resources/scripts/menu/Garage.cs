using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class Garage : MonoBehaviour
    {
        public void Entered()
        {
            StartCoroutine(StartNavigation());
        }

        private IEnumerator StartNavigation()
        {
            yield return SceneManager.LoadSceneAsync("garage");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Garage
{
    public class Back : MonoBehaviour
    {
        public void Entered()
        {
            StartCoroutine(StartNavigation());
        }

        private IEnumerator StartNavigation()
        {
            yield return SceneManager.LoadSceneAsync("menu");
        }
    }
}
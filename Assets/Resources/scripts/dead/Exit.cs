using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Dead {
    public class Exit : MonoBehaviour
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
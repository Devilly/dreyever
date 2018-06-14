﻿using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Dead
{
    public class Restart : MonoBehaviour
    {

        public void Entered()
        {
            StartCoroutine(StartNavigation());
        }

        private IEnumerator StartNavigation()
        {
            yield return SceneManager.LoadSceneAsync("rush");
        }
    }
}
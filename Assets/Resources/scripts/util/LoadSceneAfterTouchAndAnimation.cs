using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Util {
	public class LoadSceneAfterTouchAndAnimation : DoSomethingAfterTouchAndAnimation {

        public string targetScene;

        public override void StartSomethingAfterTouchAndAnimation()
        {
            StartCoroutine(StartNavigation());
        }

        private IEnumerator StartNavigation() {
			yield return new WaitForSeconds (.1f);
			SceneManager.LoadSceneAsync (targetScene);
		}
	}
}
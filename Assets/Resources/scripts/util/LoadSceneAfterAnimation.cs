using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Util {
	public class LoadSceneAfterAnimation : MonoBehaviour {

		private SpriteRenderer spriteRenderer;
		public Sprite[] animationSprites;

        private bool activated = false;

        public string targetScene;

		void Start() {
            spriteRenderer = GetComponent<SpriteRenderer> ();
		}

		public void ihavebeentouched() {
            if (activated) return;
            else activated = true;

			StartCoroutine (StartAnimation());
		}

		private IEnumerator StartAnimation() {
			float frameTime = 1f / 40;
			foreach (Sprite sprite in animationSprites) {
				yield return new WaitForSeconds (frameTime);
                spriteRenderer.sprite = sprite;
			}

            StartCoroutine(StartNavigation());
		}

		private IEnumerator StartNavigation() {
			yield return new WaitForSeconds (.1f);
			SceneManager.LoadSceneAsync (targetScene);
		}
	}
}
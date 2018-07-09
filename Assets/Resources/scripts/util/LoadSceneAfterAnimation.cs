using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Start {
	public class LoadSceneAfterAnimation : MonoBehaviour {

		private Image image;
		public Sprite[] animationSprites;

        public string targetScene;

		void Start() {
			image = GetComponent<Image> ();
		}

		public void Entered() {
			StartCoroutine (StartAnimation());
		}

		private IEnumerator StartAnimation() {
			float frameTime = 1 / 40;
			foreach (Sprite sprite in animationSprites) {
				yield return new WaitForSeconds (frameTime);
				image.sprite = sprite;
			}

            StartCoroutine(StartNavigation());
		}

		private IEnumerator StartNavigation() {
			yield return new WaitForSeconds (.1f);
			SceneManager.LoadSceneAsync (targetScene);
		}
	}
}
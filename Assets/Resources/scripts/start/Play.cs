using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Start {
	public class Play : MonoBehaviour {

		private Image image;
		public Sprite[] animationSprites;

		void Start() {
			image = GetComponent<Image> ();
		}

		public void Entered() {
			StartCoroutine (StartAnimation());
			StartCoroutine (StartNavigation ());
		}

		private IEnumerator StartAnimation() {
			float frameTime = 1 / 40;
			foreach (Sprite sprite in animationSprites) {
				yield return new WaitForSeconds (frameTime);
				image.sprite = sprite;
			}
		}

		private IEnumerator StartNavigation() {
			yield return new WaitForSeconds (.5f);
			SceneManager.LoadSceneAsync ("menu");
		}
	}
}
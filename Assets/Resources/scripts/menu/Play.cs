using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Menu {
	public class Play : MonoBehaviour {

		public void Entered() {
			StartCoroutine (StartNavigation ());
		}

		private IEnumerator StartNavigation() {
			yield return SceneManager.LoadSceneAsync ("rush");
		}
	}
}

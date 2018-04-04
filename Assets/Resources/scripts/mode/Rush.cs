using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mode {
	public class Rush : MonoBehaviour {

		public void Entered() {
			StartCoroutine (StartNavigation ());
		}

		private IEnumerator StartNavigation() {
			yield return SceneManager.LoadSceneAsync ("rush");
		}
	}
}
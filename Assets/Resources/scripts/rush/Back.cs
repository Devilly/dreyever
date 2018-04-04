﻿using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Rush {
	public class Back : MonoBehaviour {

		public void Entered() {
			StartCoroutine (StartNavigation ());
		}

		private IEnumerator StartNavigation() {
			yield return SceneManager.LoadSceneAsync ("mode");
		}
	}
}
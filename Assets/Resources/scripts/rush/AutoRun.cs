using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreyever;

namespace Rush {
	public class AutoRun : MonoBehaviour {

		public State state;

		void Start () {
			StartCoroutine (StartMovingRight ());
		}

		private IEnumerator StartMovingRight() {
			yield return new WaitForSecondsRealtime(2);
			state.StartMovingRight ();
		}
	}
}
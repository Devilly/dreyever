using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreyever;

namespace Rush {
	public class AutoRun : MonoBehaviour {

		private State state;

		void Start () {
            state = GameObject.FindGameObjectWithTag("Player").transform.Find("Monocar").GetComponent<State>();
            
            StartCoroutine (StartMovingRight ());
		}

		private IEnumerator StartMovingRight() {
			yield return new WaitForSecondsRealtime(2);
			state.StartMovingRight ();
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreyever;

namespace Rush {
	public class Jump : MonoBehaviour {

		public State state;

		void FixedUpdate () {
			if (Input.touches.Length > 0) {
				state.StartJumping ();
			} else {
				state.StopJumping ();
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreyever;

namespace Rush {
	public class Jump : MonoBehaviour {

		private State state;

        void Start()
        {
            state = GameObject.FindGameObjectWithTag("dreyever").transform.GetComponent<State>();
        }

		void FixedUpdate () {
#if UNITY_EDITOR
            if (Input.anyKey)
            {
                state.StartJumping();
            }
            else
            {
                state.StopJumping();
            }
#else
            if (Input.touches.Length > 0) {
                state.StartJumping();
			} else {
				state.StopJumping ();
			}
#endif
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Start {
	public class NoSleep : MonoBehaviour {
		
		void Start () {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
	}
}
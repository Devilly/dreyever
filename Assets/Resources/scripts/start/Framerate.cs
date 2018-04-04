using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Start {
	public class Framerate : MonoBehaviour {

		void Start () {
			Application.targetFrameRate = 60;
		}
	}
}
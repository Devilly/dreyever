using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSpace {
	public class Orientation : MonoBehaviour {

		public ScreenOrientation orientation;

		void Start () {
			Screen.orientation = orientation;
		}
	}
}
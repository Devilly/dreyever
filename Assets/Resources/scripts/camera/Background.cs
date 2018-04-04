using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSpace {
	public class Background : MonoBehaviour {

		private Camera component;

		void Start () {
			component = GetComponent<Camera> ();
			component.backgroundColor = new Color(
				convertColorIntToFloat(251), 
				convertColorIntToFloat(251),
				convertColorIntToFloat(251)
			);
		}

		private float convertColorIntToFloat(int color) {
			return 1f / 255 * color;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSpace {
	public class Follow : MonoBehaviour {

		public GameObject hero;

		void LateUpdate () {
			transform.position = new Vector3 (hero.transform.position.x, transform.position.y, transform.position.z);
		}
	}
}
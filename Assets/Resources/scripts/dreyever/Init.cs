using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreyever {
	public class Init : MonoBehaviour {
		
		public GameObject smoke;

		void Start() {
			gameObject.AddComponent<PolygonCollider2D> ();
		}
	}
}
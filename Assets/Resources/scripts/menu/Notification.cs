using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
	public class Notification : MonoBehaviour {

		private Image image;

		void Start () {
			image = GetComponent<Image> ();

			image.enabled = false;
		}
	}
}
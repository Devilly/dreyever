using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
	public class Influence {
		private float speed;

		public Influence Speed(float speed) {
			this.speed = speed;
			return this;
		}

		public float Speed() {
			return speed;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
	public abstract class InteractionBase : MonoBehaviour {

		public abstract Influence Do();
	}
}
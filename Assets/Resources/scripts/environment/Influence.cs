using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
	public class Influence {
		private float horizontalMovement;
        private float verticalMovement;
        private Vector2 reposition;

        public Influence()
        {
            reposition = new Vector2(0, 0);
        }

		public Influence HorizontalMovement(float horizontalMovement) {
			this.horizontalMovement = horizontalMovement;
			return this;
		}

		public float HorizontalMovement() {
			return horizontalMovement;
		}

        public Influence VerticalMovement(float verticalMovement)
        {
            this.verticalMovement = verticalMovement;
            return this;
        }

        public float VerticalMovement()
        {
            return verticalMovement;
        }

        public Influence Reposition(Vector2 reposition)
        {
            this.reposition = reposition;
            return this;
        }

        public Vector2 Reposition()
        {
            return reposition;
        }
    }
}

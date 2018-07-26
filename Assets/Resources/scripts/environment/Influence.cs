using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
	public class Influence {
		private float horizontalMovement;
        private float verticalMovement;
        private Vector2 reposition;

        private bool startToListen;
        private bool die;

        public Influence()
        {
            reposition = Vector2.zero;
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

        public Influence StartToListen(bool startToListen)
        {
            this.startToListen = startToListen;
            return this;
        }

        public bool StartToListen()
        {
            return startToListen;
        }

        public Influence Die(bool die)
        {
            this.die = die;
            return this;
        }

        public bool Die()
        {
            return die;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
	public class Influence {
		private float horizontalMovement;
        private float verticalMovement;
        private Vector2 reposition;
        private Vector2? place;

        private bool startToListen;
        private bool die;

        private bool startMoving;
        private bool stopMoving;
        public bool turnAround;

        public Influence()
        {
            reposition = Vector2.zero;
            place = null;
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

        public Influence Place(Vector2 place)
        {
            this.place = place;
            return this;
        }

        public Vector2? Place()
        {
            return place;
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

        public Influence StartMoving(bool startMoving)
        {
            this.startMoving = startMoving;
            return this;
        }

        public bool StartMoving()
        {
            return startMoving;
        }

        public Influence StopMoving(bool stopMoving)
        {
            this.stopMoving = stopMoving;
            return this;
        }

        public bool StopMoving()
        {
            return stopMoving;
        }

        public Influence TurnAround(bool turnAround)
        {
            this.turnAround = turnAround;
            return this;
        }

        public bool TurnAround()
        {
            return turnAround;
        }
    }
}

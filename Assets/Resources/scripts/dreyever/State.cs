using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreyever {
	public class State : MonoBehaviour {

		private Movement movement = Movement.NONE;
		private Direction direction = Direction.RIGHT;

		private bool isJumping = false;

		public void StartMovingLeft() {
			StartMoving (Direction.LEFT);
		}

		public void StartMovingRight() {
			StartMoving (Direction.RIGHT);
		}

		private void StartMoving(Direction direction) {
			movement = Movement.RUNNING;
			this.direction = direction;
		}

		public void StopMoving() {
			movement = Movement.NONE;
		}

		public void StartJumping() {
			isJumping = true;
		}

		public void StopJumping() {
			isJumping = false;
		}

		public Movement GetMovement() {
			return movement;
		}

		public Direction GetDirection() {
			return direction;
		}

		public bool IsJumping() {
			return isJumping;
		}
	}
}
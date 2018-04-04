﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using Environment.Speed;

namespace Dreyever {
	public class Controls : MonoBehaviour {
		public State state;

		public GameObject container;

		private new Collider2D collider;

		public GameObject hitbox;
		private BoxCollider2D hitboxCollider;

		private GameObject smoke;
		private Animator smokeAnimator;
		private SpriteRenderer smokeRenderer;

		private const float runningSpeed = 9f;
		private float bonusSpeed = 0f;
		private const float bonusSpeedLoss = 10f;

		private const float maximumVerticalDropSpeed = -2.0f;
		private const float gravity = 1.5f;
		private const float jumpSpeed = 0.4f;

		private bool standsOnTheGround = false;
		private float verticalSpeed = 0f;

		public const float safetyRing = 0.01f;

		private string[] collisionLayers = new string[]{ "environment" };

		void Start() {
			collider = GetComponent<PolygonCollider2D> ();

			hitboxCollider = hitbox.GetComponent<BoxCollider2D> ();

			smoke = GetComponent<Init> ().smoke;
			smokeAnimator = smoke.GetComponent<Animator> ();
			smokeRenderer = smoke.GetComponent<SpriteRenderer> ();
			smokeRenderer.enabled = false;
		}

		void FixedUpdate() {
			AdaptLooks ();
			AdaptPhysics ();

			MoveHorizontal ();
			MoveVertical ();
		}

		void AdaptLooks() {
			Movement currentMovement = state.GetMovement();
			Direction currentDirection = state.GetDirection ();

			// Turn dreyever into the right direction.
			float rotation = 0;
			if (currentMovement == Movement.RUNNING) {
				rotation = -22;
			}

			if (currentDirection == Direction.LEFT) {
				rotation *= -1;
			}

			transform.eulerAngles = new Vector3 (0, 0, rotation);

			// Show smoke appropriately.
			if (standsOnTheGround && (currentMovement == Movement.RUNNING)) {
				smokeRenderer.enabled = true;
				smokeAnimator.Play("smoke");

				int smokeShift = 0;
				if (currentDirection == Direction.LEFT) {
					smokeRenderer.flipX = false;
					smokeShift = 1;
				} else if (currentDirection == Direction.RIGHT) {
					smokeRenderer.flipX = true;
					smokeShift = -1;
				}

				smoke.transform.position = new Vector3 (collider.bounds.center.x + collider.bounds.extents.x * smokeShift,
					collider.bounds.min.y, 0);
			} else {
				smokeRenderer.enabled = false;
			}
		}

		void AdaptPhysics() {
			Bounds realBounds = collider.bounds;
			float bottomSpacing = 0.05f;

			Vector2 size = ((Vector2) realBounds.size) - new Vector2 (0, bottomSpacing);
			hitboxCollider.size = size;

			Bounds hitboxBounds = hitboxCollider.bounds;
			hitboxCollider.offset = hitboxCollider.offset + new Vector2 (realBounds.min.x - hitboxBounds.min.x, realBounds.max.y - hitboxBounds.max.y);
		}

		void MoveHorizontal() {
			Movement currentMovement = state.GetMovement();

			if (standsOnTheGround) {
				bonusSpeed -= bonusSpeedLoss * Time.fixedDeltaTime;
				if (bonusSpeed < 0f) {
					bonusSpeed = 0f;
				}
			}

			Direction currentDirection = Direction.RIGHT;
			if (state.GetDirection () == Direction.LEFT) {
				currentDirection = Direction.LEFT;
			}

			float movementDistance = 0;
			if (currentMovement == Movement.RUNNING) {
				movementDistance = (runningSpeed + bonusSpeed) * Time.fixedDeltaTime;
			}

			if (currentDirection == Direction.LEFT) {
				movementDistance *= -1;
			}

			Vector2 movementVector = new Vector2 (movementDistance, 0);

			RaycastHit2D[] hits = new RaycastHit2D[1];
			hitboxCollider.Cast (movementVector, hits);
			RaycastHit2D hit = hits [0];

			if (hit.collider != null) {
				float maximumDistance = movementVector.x < 0 ? -hit.distance + safetyRing : hit.distance - safetyRing;
				if (Mathf.Abs (maximumDistance) < Mathf.Abs (movementVector.x)) {
					movementVector = new Vector2 (maximumDistance, 0);
				}
			}

			Move(movementVector.x, 0);
		}

		void MoveVertical() {
			bool isCurrentlyJumping = state.IsJumping ();

			if (standsOnTheGround && isCurrentlyJumping) {
				standsOnTheGround = false;
				verticalSpeed = jumpSpeed;
			}

			verticalSpeed -= gravity * Time.fixedDeltaTime;
			if (verticalSpeed < maximumVerticalDropSpeed) {
				verticalSpeed = maximumVerticalDropSpeed;
			}
				
			Vector2 movementVector = new Vector2 (0, verticalSpeed);

			RaycastHit2D[] hits = new RaycastHit2D[1];
			hitboxCollider.Cast (movementVector, hits);
			RaycastHit2D hit = hits [0];

			if (hit.collider != null) {
				bool hasTouchedFloor = false;

				float maximumDistance = movementVector.y < 0 ? -hit.distance + safetyRing : hit.distance - safetyRing;
				if (hit.distance == 0) {
					movementVector = new Vector2 (0, 0);
					hasTouchedFloor = true;
				} else if (Mathf.Abs (maximumDistance) < Mathf.Abs (movementVector.y)) {
					movementVector = new Vector2 (0, maximumDistance);
					hasTouchedFloor = true;
				}

				if (hasTouchedFloor) {
					if (movementVector.y < 0) {
						// movementVector.y smaller than 0 means a falling movement.
						// Therefore when touching the floor it's on the upperside and the dreyever stands again.
						standsOnTheGround = true;
					}

					verticalSpeed = 0f;
				} else {
					standsOnTheGround = false;
				}
			} else {
				standsOnTheGround = false;
			}

			Move(0, movementVector.y);

			if (standsOnTheGround) {
				RaycastHit2D[] floorHits = Physics2D.BoxCastAll(hitboxCollider.bounds.center, hitboxCollider.bounds.size,
					0, Vector2.down,
					safetyRing,
					LayerMask.GetMask(collisionLayers));

				foreach(RaycastHit2D floorHit in floorHits) {
					if (floorHit.collider != null) {
						InteractionBase interact = floorHit.rigidbody.GetComponent<InteractionBase> ();
						if (interact != null) {
							Influence influence = interact.Do ();
							bonusSpeed += influence.Speed ();
						}
					}
				}
			}
		}

		public void Move(float x, float y) {
			Vector3 positionVector = new Vector2 (container.transform.position.x + x,
				container.transform.position.y + y);

			container.transform.position = positionVector;
		}

		public void ForceJump(float jumpSpeed) {
			this.verticalSpeed = jumpSpeed;
		}
	}
}
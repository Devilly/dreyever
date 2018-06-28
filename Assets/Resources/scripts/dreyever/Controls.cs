using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using Environment.Speed;

namespace Dreyever {
	public class Controls : MonoBehaviour {
		public State state;

		public GameObject container;

        public new PolygonCollider2D collider;

		public GameObject hitbox;
		private BoxCollider2D hitboxCollider;

		private const float runningSpeed = 9f;
		private float bonusSpeed = 0f;
		private const float bonusSpeedLoss = 10f;

		private const float maximumVerticalDropSpeed = -2.0f;
		private const float gravity = 1.5f;
		private const float jumpSpeed = 0.4f;

		private bool grounded = false;
		private float verticalSpeed = 0f;

		public const float safetyRing = 0.01f;

		private string[] collisionLayers = new string[]{ "environment" };

		void Start() {
            collider = gameObject.AddComponent<PolygonCollider2D>();

            hitboxCollider = hitbox.GetComponent<BoxCollider2D> ();
        }

		void FixedUpdate() {
			MoveHorizontal ();
			MoveVertical ();
            
            AdaptLooks();
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
			if (grounded) {
				bonusSpeed -= bonusSpeedLoss * Time.deltaTime;
				if (bonusSpeed < 0f) {
					bonusSpeed = 0f;
				}
			}

            Movement currentMovement = state.GetMovement();
            float naturalMovementDistance = 0;
			if (currentMovement == Movement.RUNNING) {
				naturalMovementDistance = (runningSpeed + bonusSpeed) * Time.deltaTime;
			}

			Vector2 movementVector = new Vector2 (naturalMovementDistance, 0);

            RaycastHit2D hit = Physics2D.BoxCast(hitboxCollider.bounds.center, hitboxCollider.bounds.size,
                0, movementVector, Mathf.Abs(movementVector.x), LayerMask.GetMask(collisionLayers));

            if (hit.collider != null)
            {
                float maximumMovementDistance = CalculateMaximumDistance(naturalMovementDistance, hit);
                movementVector = new Vector2(maximumMovementDistance, 0);

                if (Mathf.Abs(maximumMovementDistance) < Mathf.Abs(naturalMovementDistance))
                {
                    Vector2? alternativeMovementVector = CalculateAcceptableVerticalLiftMovementVector(maximumMovementDistance, naturalMovementDistance);

                    if(alternativeMovementVector.HasValue)
                    {
                        movementVector = (Vector2) alternativeMovementVector;
                    }
                }
			}

			Move(movementVector);
		}

        private float CalculateMaximumDistance(float naturalMovementDistance, RaycastHit2D hit)
        {
            if(hit.collider == null)
            {
                return naturalMovementDistance;
            } else
            {
                float distanceToCollision = state.GetDirection() == Direction.LEFT ? -hit.distance + safetyRing : hit.distance - safetyRing;
                return Mathf.Abs(distanceToCollision) < Mathf.Abs(naturalMovementDistance) ? distanceToCollision : naturalMovementDistance;
            }
        }

        // This code tries to find a direct horizontal path that is unobscured and as close as possible above any hindrances.
        // What would also be possible is to make codes which takes a leap over anything high, e.g. 0.1,
        // and after that dropping down until hitting the ground again.
        private Vector2? CalculateAcceptableVerticalLiftMovementVector(float maximumMovementDistance, float naturalMovementDistance)
        {
            RaycastHit2D hit;
            float alternativeMovementDistance = 0;

            const float liftStep = safetyRing;
            float lift = liftStep;
            bool found = false;

            while ((lift <= 0.1) && !found)
            {
                hit = Physics2D.BoxCast(hitboxCollider.bounds.center + new Vector3(0, lift, 0), hitboxCollider.bounds.size,
                    0, new Vector2(naturalMovementDistance, 0), Mathf.Infinity, LayerMask.GetMask(collisionLayers));

                alternativeMovementDistance = CalculateMaximumDistance(naturalMovementDistance, hit);
                if (alternativeMovementDistance > maximumMovementDistance)
                {
                    found = true;
                }

                lift += liftStep;
            }

            if(found)
            {
                return new Vector2(alternativeMovementDistance, lift + safetyRing);
            }
            return null;
        }

		void MoveVertical() {
			bool isCurrentlyJumping = state.IsJumping ();

			if (grounded && isCurrentlyJumping) {
				grounded = false;
				verticalSpeed = jumpSpeed;
			}

			verticalSpeed -= gravity * Time.deltaTime;
			if (verticalSpeed < maximumVerticalDropSpeed) {
				verticalSpeed = maximumVerticalDropSpeed;
			}
				
			Vector2 movementVector = new Vector2 (0, verticalSpeed);

			RaycastHit2D hit = Physics2D.BoxCast(hitboxCollider.bounds.center, hitboxCollider.bounds.size,
                0, movementVector, Mathf.Abs(movementVector.y), LayerMask.GetMask(collisionLayers));

			if (hit.collider != null) {
				bool touchedFloor = false;

				float maximumDistance = movementVector.y < 0 ? -hit.distance + safetyRing : hit.distance - safetyRing;
				if (hit.distance == 0) {
					movementVector = new Vector2 (0, 0);
					touchedFloor = true;
				} else if (Mathf.Abs (maximumDistance) < Mathf.Abs (movementVector.y)) {
					movementVector = new Vector2 (0, maximumDistance);
					touchedFloor = true;
				}

				if (touchedFloor) {
					if (verticalSpeed < 0) {
						grounded = true;
					}

					verticalSpeed = 0f;
				} else {
					grounded = false;
				}
			} else {
				grounded = false;
			}

			Move(movementVector);

			if (grounded) {
				RaycastHit2D[] floorHits = Physics2D.BoxCastAll(hitboxCollider.bounds.center, hitboxCollider.bounds.size,
					0, Vector2.down, safetyRing, LayerMask.GetMask(collisionLayers));

				foreach(RaycastHit2D floorHit in floorHits) {
					if (floorHit.collider != null) {
						InteractionBase interact = floorHit.rigidbody.GetComponent<InteractionBase> ();
						if (interact != null) {
							Influence(interact.Do ());
						}
					}
				}
			}
		}

        public void Influence(Influence influence)
        {
            bonusSpeed += influence.HorizontalMovement();
            verticalSpeed += influence.VerticalMovement();

            Move(new Vector2(influence.Reposition().x, influence.Reposition().y));
        }

		private void Move(Vector2 movementVector)
        {
            container.transform.position = new Vector2(container.transform.position.x + movementVector.x,
                container.transform.position.y + movementVector.y);

            AdaptPhysics();
        }
	}
}
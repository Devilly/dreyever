using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using Environment.Speed;
using Scriptables.Util;

namespace Dreyever {
	public class Controls : MonoBehaviour {

        public Instantiater instantiater;

        public State state;
        public Animator animator;

        public Afterimaging afterimaging;

        private new BoxCollider2D collider;

        private const float runningSpeed = 9f;
		private float bonusSpeed = 0f;
		private const float bonusSpeedLoss = 10f;

		private const float maximumVerticalDropSpeed = -2.0f;
		private const float gravity = 1.5f;
		private const float jumpSpeed = 0.4f;
        
		private bool grounded = false;
        private bool jumpReady = false;
        private bool airturned = false;
		private float verticalSpeed = 0f;

		public const float safetyRing = 0.01f;

        private bool startedToListen = false;
        private bool dead = false;

		private string[] collisionLayers = new string[]{ "environment" };

        void Start()
        {
            collider = GetComponent<BoxCollider2D>();
        }

		void FixedUpdate() {
            if (dead) return;

			MoveHorizontal ();
			MoveVertical ();
        }

		void MoveHorizontal() {
			if (grounded) {
				bonusSpeed -= bonusSpeedLoss * Time.fixedDeltaTime;
				if (bonusSpeed < 0f) {
					bonusSpeed = 0f;
				}
			}

            float naturalMovementDistance = 0;

            Movement currentMovement = state.GetMovement();
			if (currentMovement == Movement.RUNNING) {
				naturalMovementDistance = (runningSpeed + bonusSpeed) * Time.fixedDeltaTime;
			}

            if(grounded && currentMovement == Movement.RUNNING)
            {
                animator.StartAnimation(Animation.TILT, state.GetDirection(), () =>
                {
                    jumpReady = true;
                });
            }

            Direction currentDirection = state.GetDirection();
            if(currentDirection == Direction.LEFT)
            {
                naturalMovementDistance *= -1;
            }

            Vector2 movementVector = new Vector2 (naturalMovementDistance, 0);

            RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size,
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
                hit = Physics2D.BoxCast(collider.bounds.center + new Vector3(0, lift, 0), collider.bounds.size,
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
			bool isCurrentlyJumping = startedToListen && state.IsJumping ();

			if (grounded && jumpReady && isCurrentlyJumping) {
                grounded = false;
                jumpReady = false;
                verticalSpeed = jumpSpeed;
            }

            float previousVerticalSpeed = verticalSpeed;
			verticalSpeed -= gravity * Time.fixedDeltaTime;

            float verticalSpeedAirturnMoment = .15f;
            if((previousVerticalSpeed >= verticalSpeedAirturnMoment) &&
                (verticalSpeed <= verticalSpeedAirturnMoment))
            {
                animator.StartAnimation(Animation.AIRTURN, state.GetDirection(), () =>
                {
                    airturned = true;
                });
            }

			if (verticalSpeed < maximumVerticalDropSpeed) {
				verticalSpeed = maximumVerticalDropSpeed;
			}
				
			Vector2 movementVector = new Vector2 (0, verticalSpeed);

			RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size,
                0, movementVector, Mathf.Abs(movementVector.y), LayerMask.GetMask(collisionLayers));

            bool wasGrounded = grounded;

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

            if(grounded && !wasGrounded)
            {
                if(airturned)
                {
                    airturned = false;
                    animator.StartAnimation(Animation.LANDING, state.GetDirection());
                } else
                {
                    jumpReady = true;
                }
            }

			Move(movementVector);

			if (grounded) {
				RaycastHit2D[] floorHits = Physics2D.BoxCastAll(collider.bounds.center, collider.bounds.size,
					0, Vector2.down, safetyRing, LayerMask.GetMask(collisionLayers));

				foreach(RaycastHit2D floorHit in floorHits) {
					if (floorHit.collider != null) {
                        if(floorHit.rigidbody != null)
                        {
                            InteractionBase interact = floorHit.rigidbody.GetComponent<InteractionBase>();
                            if (interact != null)
                            {
                                Influence(interact.Do());
                            }
                        }
					}
				}
			}
        }

        public void Influence(Influence influence)
        {
            if(influence.StartToListen())
            {
                startedToListen = true;
            }

            if(influence.Die())
            {
                dead = true;
                animator.StartAnimation(Animation.EXPLOSION, state.GetDirection(), () =>
                {
                    afterimaging.StopShowingAfterimages();
                    instantiater.Clone();
                });
            }

            bonusSpeed += influence.HorizontalMovement();
            verticalSpeed += influence.VerticalMovement();

            Move(new Vector2(influence.Reposition().x, influence.Reposition().y));
        }

		private void Move(Vector2 movementVector)
        {
            transform.position = new Vector2(transform.position.x + movementVector.x,
                transform.position.y + movementVector.y);
        }
	}
}
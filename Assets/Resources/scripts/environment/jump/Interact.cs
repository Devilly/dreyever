using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Dreyever;

namespace Environment.Jump {
	public class Interact : InteractionBase {

		private const float movementDown = 0.35f;
		private const float timeToMoveDown = 0.1f;

		private const float movementUp = 0.6f;
		private const float timeToMoveUp = 0.05f;

		private const float timeToReset = 2f;

        public float transmittedVerticalMovement = 0.6f;

        private new SpriteRenderer renderer;

		private new Collider2D collider;

		public Sprite[] sprites;

		private enum Status {
			IDLE, INTERMEDIATE_STATE, LOAD, FIRE, RESET
		}
		private Status status = Status.IDLE;
		private float startingPosition;
		private float intermediateStartingPosition;

		void Start() {
			renderer = GetComponent<SpriteRenderer> ();
			collider = GetComponent<BoxCollider2D> ();

			startingPosition = transform.position.y;
		}

		void FixedUpdate() {
			if (status == Status.LOAD) {
				float newY = transform.position.y - (1 / timeToMoveDown) * movementDown * Time.fixedDeltaTime;
				if (newY <= intermediateStartingPosition - movementDown) {
					newY = intermediateStartingPosition - movementDown;

					status = Status.INTERMEDIATE_STATE;
					renderer.sprite = sprites [0];

					StartCoroutine (StartFire ());
				}
				transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
			} else if (status == Status.FIRE) {
				bool isCatapulted = false;

				float deltaY = (1 / timeToMoveUp) * movementUp * Time.fixedDeltaTime;
				float newY = transform.position.y + deltaY;

				if (newY >= intermediateStartingPosition + movementUp) {
					newY = intermediateStartingPosition + movementUp;
					deltaY = newY - transform.position.y;

					status = Status.INTERMEDIATE_STATE;
					renderer.sprite = sprites [2];

					StartCoroutine (StartReset ());

					isCatapulted = true;
				} else if (newY >= intermediateStartingPosition + movementUp / 2) {
					renderer.sprite = sprites [1];
				}

				RaycastHit2D[] hits = new RaycastHit2D[100];
				collider.Cast (Vector2.up, hits);
				IEnumerable<RaycastHit2D> trackedBodies = from hit in hits
				                                          where hit.collider != null && hit.collider.tag == "dreyever" && hit.distance < deltaY
				                                          select hit;


				if (trackedBodies.Count () > 0) {
					trackedBodies.ToList ().ForEach (hit => {
                        hit.transform.SendMessage("Influence", new Influence().Reposition(new Vector2(0, deltaY - hit.distance + Controls.safetyRing)));

						if (isCatapulted) {
                            hit.transform.SendMessage("Influence", new Influence().VerticalMovement(transmittedVerticalMovement));
						}
					});
				}

				transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
			} else if (status == Status.RESET) {
				float deltaY = (1 / timeToReset) * (startingPosition - intermediateStartingPosition) * Time.fixedDeltaTime;
				float newY = transform.position.y + deltaY;

				if (newY <= startingPosition) {
					newY = startingPosition;

					status = Status.IDLE;
					renderer.sprite = sprites [3];
				}

				transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
			}
		}

		private IEnumerator StartFire() {
			yield return new WaitForSeconds (0.15f);
			status = Status.FIRE;
			intermediateStartingPosition = transform.position.y;
		}

		private IEnumerator StartReset() {
			yield return new WaitForSeconds (1f);
			status = Status.RESET;
			intermediateStartingPosition = transform.position.y;
		}
		
		public override Influence Do() {
			if (status == Status.IDLE) {
				status = Status.LOAD;
				intermediateStartingPosition = transform.position.y;
			}

			return new Influence ();
		}
	}
}
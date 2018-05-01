using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Speed {
	public class Interact : InteractionBase {

		private new SpriteRenderer renderer;
		private IEnumerator coroutine;

		public Sprite[] sprites;

		void Start() {
			renderer = GetComponent<SpriteRenderer> ();
		}

		public override Influence Do() {
			Influence influence = new Influence();
			if (coroutine == null) {
				influence = influence.HorizontalMovement (2f);
			} else {
				StopCoroutine (coroutine);
			}
            
			StartCoroutine (coroutine = ChangeSprite());

			return influence;
		}

		private IEnumerator ChangeSprite() {
			renderer.sprite = sprites[0];

			float delayPerSprite = .05f;
			for (int index = 1; index < sprites.Length; index++) {
				yield return new WaitForSeconds(delayPerSprite);
				renderer.sprite = sprites[index];
			}
            
            coroutine = null;
        }
	}
}
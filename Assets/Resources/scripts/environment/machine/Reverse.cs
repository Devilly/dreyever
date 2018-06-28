using Dreyever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Machine
{
    public class Reverse : MonoBehaviour
    {
        public new SpriteRenderer renderer;
        public Sprite inactive;
        public Sprite active;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.name == "Hitbox")
            {
                State state = collider.gameObject.transform.parent.GetComponentInChildren<State>();
                StartCoroutine(TurnAround(state));
            }
        }

        private IEnumerator TurnAround(State state)
        {
            state.StopMoving();
            renderer.sprite = active;

            yield return new WaitForSeconds(1);

            state.TurnAround();
            state.StartMoving();
            renderer.sprite = inactive;
        }
    }
}
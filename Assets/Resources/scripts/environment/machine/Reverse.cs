using Dreyever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Machine
{
    public class Reverse : MonoBehaviour
    {
        public new SpriteRenderer symbol;

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
            symbol.sprite = active;

            yield return new WaitForSeconds(1);

            state.TurnAround();
            state.StartMoving();
            symbol.sprite = inactive;
        }
    }
}
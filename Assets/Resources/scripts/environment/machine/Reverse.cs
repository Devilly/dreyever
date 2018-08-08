using Dreyever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Machine
{
    public class Reverse : MonoBehaviour
    {
        public SpriteRenderer symbol;

        public Sprite inactive;
        public Sprite active;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "dreyever")
            {
                StartCoroutine(TurnAround(collider));
            }
        }

        private IEnumerator TurnAround(Collider2D collider)
        {
            collider.SendMessage("Influence", new Influence().StopMoving(true));
            symbol.sprite = active;

            yield return new WaitForSeconds(1);

            collider.SendMessage("Influence", new Influence()
                .TurnAround(true)
                .StartMoving(true));
            symbol.sprite = inactive;
        }
    }
}
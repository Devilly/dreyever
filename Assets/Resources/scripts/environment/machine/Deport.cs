using Dreyever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Machine
{
    public class Deport : MonoBehaviour
    {

        public SpriteRenderer symbol;

        public Sprite inactive;
        public Sprite active;

        public GameObject[] destinations;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.name == "Hitbox")
            {
                Transform monocar = collider.gameObject.transform.parent.Find("Monocar");
                StartCoroutine(TurnAround(monocar));
            }
        }

        private IEnumerator TurnAround(Transform monocar)
        {
            State state = monocar.GetComponent<State>();

            state.StopMoving();
            symbol.sprite = active;

            yield return new WaitForSeconds(2);

            monocar.parent.position = destinations[new System.Random().Next(0, destinations.Length)].transform.position;

            yield return new WaitForSeconds(2);

            state.StartMoving();
            symbol.sprite = inactive;
        }
    }
}
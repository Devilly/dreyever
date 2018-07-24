using Dreyever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Environment.Factory
{
    public class Behaviour : MonoBehaviour
    {
        public GameObject dreyeverPrefab;
        public Sprite[] animationSprites;

        private State state;
        private new SpriteRenderer renderer;

        private bool activated = false;

        void Start()
        {
            GameObject dreyever = Instantiate(dreyeverPrefab, transform.position, Quaternion.identity);
            state = dreyever.transform.Find("Monocar").GetComponent<State>();

            renderer = GetComponent<SpriteRenderer>();
        }

        void FixedUpdate()
        {
            if (activated) return;
            if (Input.touches.Length == 0) return;

            Touch touch = Input.GetTouch(0);
            Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

            if(hit.collider != null)
            {
                if(hit.collider.name == "Factory")
                {
                    StartCoroutine(StartAnimation());
                    activated = true;
                }
            }
        }

        private IEnumerator StartAnimation()
        {
            float frameTime = 1 / 40;
            foreach (Sprite sprite in animationSprites)
            {
                yield return new WaitForSeconds(frameTime);
                renderer.sprite = sprite;
            }

            StartCoroutine(StartMovingRight());
        }

        private IEnumerator StartMovingRight()
        {
            yield return new WaitForSecondsRealtime(2);
            state.StartMovingRight();
        }
    }
}
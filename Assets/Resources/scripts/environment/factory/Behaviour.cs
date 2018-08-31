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

        public float manufacturingTime = 1;

        void Start()
        {
            GameObject dreyever = Instantiate(dreyeverPrefab, transform.position, Quaternion.identity);
            state = dreyever.GetComponent<State>();

            renderer = GetComponent<SpriteRenderer>();
        }

        public void ihavebeentouched()
        {
            if (activated) return;

            StartCoroutine(StartAnimation());
            activated = true;
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
            yield return new WaitForSecondsRealtime(manufacturingTime);
            state.StartMovingRight();
            yield return new WaitForSecondsRealtime(.5f);
            state.SendMessage("Influence", new Influence().StartToListen(true));
        }
    }
}
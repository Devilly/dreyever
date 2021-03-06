﻿using Dreyever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreyever
{
    public class Afterimaging : MonoBehaviour
    {
        private Coroutine coroutine;

        void Start()
        {
            coroutine = StartCoroutine(AddAfterimages());
        }

        public void StopShowingAfterimages()
        {
            StopCoroutine(coroutine);

            foreach(GameObject afterimage in GameObject.FindGameObjectsWithTag("afterimage"))
            {
                Destroy(afterimage);
            }
        }

        private IEnumerator AddAfterimages()
        {
            while (true)
            {
                yield return new WaitForSeconds(.05f);

                GameObject afterimage = Object.Instantiate(gameObject, transform.position, transform.rotation);
                Component[] components = afterimage.GetComponents<Component>();
                foreach(Component component in components) {
                    System.Type type = component.GetType();
                    if (type != typeof(Transform) && type != typeof(SpriteRenderer))
                    {
                        Destroy(component);
                    }

                    if(type == typeof(SpriteRenderer))
                    {
                        ((SpriteRenderer)component).sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    }
                }

                afterimage.AddComponent<AfterimageFading>();
                afterimage.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
                afterimage.tag = "afterimage";
            }
        }

        class AfterimageFading : MonoBehaviour
        {
            private new SpriteRenderer renderer;

            private void Start()
            {
                renderer = GetComponent<SpriteRenderer>();

                StartCoroutine(Fade());
            }

            private IEnumerator Fade()
            {
                renderer.color -= new Color(0, 0, 0, .9f);

                while(renderer.color.a > .05)
                {
                    renderer.color -= new Color(0, 0, 0, .01f);
                    yield return new WaitForSeconds(.05f);
                }

                Destroy(gameObject);
            }
        }
    }
}
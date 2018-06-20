using Dreyever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreyever
{
    public class Afterimaging : MonoBehaviour
    {

        void Start()
        {
            StartCoroutine(AddAfterimages());
        }

        private IEnumerator AddAfterimages()
        {
            while (true)
            {
                yield return new WaitForSeconds(.05f);

                GameObject afterimage = Object.Instantiate(gameObject, transform.position, transform.rotation);
                Destroy(afterimage.GetComponent<State>());
                Destroy(afterimage.GetComponent<Controls>());
                Destroy(afterimage.GetComponent<Afterimaging>());
                Destroy(afterimage.GetComponent<PolygonCollider2D>());

                afterimage.AddComponent<AfterimageFading>();
                afterimage.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
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
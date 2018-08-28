using Scriptables.Util;
using System.Collections;
using UnityEngine;

namespace Dead
{
    public class Behavior : MonoBehaviour
    {
        public GameObject deathScreenPrefab;
        private GameObject deathScreenInstance;

        private const float secondsBeforePositioning = 0.1f;
        private bool readyForPositioning = false;
        private bool readyForMovement = false;

        void Start()
        {
            deathScreenInstance = Instantiate(deathScreenPrefab);

            StartCoroutine(StopTime());
        }

        // Use LateUpdate so a call to activate() will be picked up in the same frame.
        void LateUpdate()
        {
            if (readyForMovement)
            {
                Vector3 bottomLeftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, 0));
                if (deathScreenInstance.transform.position.x > bottomLeftPoint.x)
                {
                    Vector3 movement = new Vector3(-12f * Time.unscaledDeltaTime, 0, 0);
                    transform.position += movement;
                    deathScreenInstance.transform.position += movement;
                }
            }
            else if (readyForPositioning)
            {
                Vector3 bottomRightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0));
                transform.position = new Vector3(bottomRightPoint.x, bottomRightPoint.y, 0);

                deathScreenInstance.transform.position = transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);

                readyForMovement = true;
            }
        }

        private IEnumerator StopTime()
        {
            yield return new WaitForSeconds(.5f);

            Time.timeScale = 0;
            readyForPositioning = true;
        }
    }
}
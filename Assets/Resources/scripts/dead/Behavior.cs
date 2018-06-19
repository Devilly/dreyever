using System.Collections;
using UnityEngine;

namespace Dead
{
    public class Behavior : MonoBehaviour
    {

        public GameObject deathScreen;

        private const float secondsBeforePositioning = 0.1f;
        private bool readyForPositioning = false;
        private bool readyForMovement = false;

        public void Activate()
        {
            StartCoroutine(StopTime());
        }

        // Use LateUpdate so a call to activate() will be picked up in the same frame.
        void LateUpdate()
        {
            if (readyForMovement)
            {
                Vector3 bottomLeftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, 0));
                if (deathScreen.transform.position.x > bottomLeftPoint.x)
                {
                    Vector3 movement = new Vector3(-12f * Time.unscaledDeltaTime, 0, 0);
                    transform.position += movement;
                    deathScreen.transform.position += movement;
                }
            }
            else if (readyForPositioning)
            {
                Vector3 bottomRightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0));
                transform.position = new Vector3(bottomRightPoint.x, bottomRightPoint.y, 0);

                deathScreen.transform.position = transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);

                readyForMovement = true;
            }
        }

        private IEnumerator StopTime()
        {
            int secondsToStop = 2;
            int steps = 20;

            for (int repetition = 0; repetition < steps; repetition++)
            {
                yield return new WaitForSecondsRealtime(secondsToStop / steps);

                float newTimeScale = Time.timeScale - 0.05f;
                if (newTimeScale < 0) newTimeScale = 0;
                Time.timeScale = newTimeScale;
            }

            readyForPositioning = true;
        }
    }
}
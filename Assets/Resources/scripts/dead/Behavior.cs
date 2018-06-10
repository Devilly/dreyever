using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour {

    public GameObject deathScreen;

    private bool activated = false;

    private const float secondsBeforePositioning = 0.1f;
    private bool readyForPositioning = false;
    private bool readyForMovement = false;
	
	public void activate()
    {
        Time.timeScale = 0;
        activated = true;

        StartCoroutine(WaitSecondsBeforePositioning());
    }

    // Use LateUpdate so a call to activate() will be picked up in the same frame.
    void LateUpdate()
    {
        if(readyForMovement)
        {
            Vector3 bottomLeftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, 0));
            if (deathScreen.transform.position.x > bottomLeftPoint.x)
            {
                Vector3 movement = new Vector3(-6f * Time.unscaledDeltaTime, 0, 0);
                transform.position += movement;
                deathScreen.transform.position += movement;
            }
        } else if(readyForPositioning)
        {
            Vector3 bottomRightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0));
            transform.position = new Vector3(bottomRightPoint.x, bottomRightPoint.y, 0);

            deathScreen.transform.position = transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);

            readyForMovement = true;
        }
    }

    private IEnumerator WaitSecondsBeforePositioning()
    {
        yield return new WaitForSecondsRealtime(secondsBeforePositioning);
        readyForPositioning = true;
    }
}

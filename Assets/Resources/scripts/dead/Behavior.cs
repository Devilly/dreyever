using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour {

    public GameObject deathScreen;

    private bool activated = false;

    private int frameCounter = 0;
    private const int framesBeforeMovement = 10;
    private bool readyForMovement = false;
	
	public void activate()
    {
        Time.timeScale = 0;
        activated = true;
    }

    // Use LateUpdate so a call to activate() will be picked up in the same frame.
    void LateUpdate()
    {
        if(activated && (frameCounter < framesBeforeMovement) && !readyForMovement)
        {
            frameCounter++;
        } else if(activated && (frameCounter == framesBeforeMovement) && !readyForMovement)
        {
            Vector3 bottomRightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0));
            transform.position = new Vector3(bottomRightPoint.x, bottomRightPoint.y, 0);

            deathScreen.transform.position = transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);

            readyForMovement = true;
        } else if (readyForMovement)
        {
            Vector3 bottomLeftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, 0));
            if(deathScreen.transform.position.x > bottomLeftPoint.x)
            {
                Vector3 movement = new Vector3(-.1f, 0, 0);
                transform.position += movement;
                deathScreen.transform.position += movement;
            }
        }
    }
}

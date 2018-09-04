using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCameraWantedAspectRatio : MonoBehaviour {
    
    private float desiredAspectRatio;
    private float screenAspectRatio;

    void Start () {
        desiredAspectRatio = 16f / 9;
        screenAspectRatio = (float) Screen.width / Screen.height;

        SetCameraRect();
    }

    private void SetCameraRect()
    {
        Rect cameraRect;

        // The screen is too high.
        if(screenAspectRatio < desiredAspectRatio)
        {
            float height = Screen.width / desiredAspectRatio;
            cameraRect = new Rect(0, (Screen.height - height) / 2, Screen.width, height);
        }
        // The screen is too wide;
        else
        {
            float width = Screen.height * desiredAspectRatio;
            cameraRect = new Rect((Screen.width - width) / 2, 0, width, Screen.height);
        }

        Camera.main.pixelRect = cameraRect;
    }
}

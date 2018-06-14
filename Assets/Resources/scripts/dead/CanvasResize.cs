using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dead
{
    public class CanvasResize : MonoBehaviour
    {

        void Start()
        {
            RectTransform transform = GetComponent<RectTransform>();

            float halfHeight = transform.sizeDelta.y / 2;
            float verticalScale = Camera.main.orthographicSize / halfHeight;

            float width = transform.sizeDelta.x;
            float smallerDimension, biggerDimension;
            if (Screen.width < Screen.height)
            {
                smallerDimension = Screen.width;
                biggerDimension = Screen.height;
            }
            else
            {
                smallerDimension = Screen.height;
                biggerDimension = Screen.width;
            }
            float futureWidth = Camera.main.orthographicSize * 2 / smallerDimension * biggerDimension;
            float horizontalScale = futureWidth / width;

            transform.localScale = new Vector3(transform.localScale.x * horizontalScale,
                transform.localScale.y * verticalScale, 1);
        }
    }
}
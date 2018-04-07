using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasResize : MonoBehaviour {
    
	void Start () {
        RectTransform transform = GetComponent<RectTransform>();

        float halfHeight = transform.sizeDelta.y / 2;
        float verticalScale = Camera.main.orthographicSize / halfHeight;

        float width = transform.sizeDelta.x;
        float futureWidth = Camera.main.orthographicSize * 2 / Screen.height * Screen.width;
        float horizontalScale = futureWidth / width;
            
        transform.localScale = new Vector3(transform.localScale.x * horizontalScale,
            transform.localScale.y * verticalScale, 1);
    }
}

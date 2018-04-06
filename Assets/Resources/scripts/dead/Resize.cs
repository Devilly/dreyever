using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour {
    
	void Start () {
        float halfHeight;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if(renderer != null)
        {
            halfHeight = renderer.bounds.extents.y;
        } else
        {
            RectTransform transform = GetComponent<RectTransform>();
            halfHeight = transform.sizeDelta.y / 2;
        }

        float scaleToScreenFit = Camera.main.orthographicSize / halfHeight; ;
        transform.localScale = new Vector3(transform.localScale.x * scaleToScreenFit,
                transform.localScale.y * scaleToScreenFit, 1);
    }
}

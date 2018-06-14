using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dead
{
    public class SpriteResize : MonoBehaviour
    {

        void Start()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            float halfHeight = renderer.bounds.extents.y;
            float scaleToScreenFit = Camera.main.orthographicSize / halfHeight;
            transform.localScale = new Vector3(transform.localScale.x * scaleToScreenFit,
                    transform.localScale.y * scaleToScreenFit, 1);
        }
    }
}
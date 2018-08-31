using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Garage
{
    public class ImageDreyever : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            SetSprite();
        }

        void Update()
        {
            SetSprite();    
        }

        void SetSprite()
        {
            spriteRenderer.sprite = Persistent.Environment.instance.GetCurrentDreyever().sprite;
        }
    }
}
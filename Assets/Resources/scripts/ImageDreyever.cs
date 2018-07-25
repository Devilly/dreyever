using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Garage
{
    public class ImageDreyever : MonoBehaviour
    {
        private Image image;
        
        void Start()
        {
            image = GetComponent<Image>();
            image.preserveAspect = true;

            SetSprite();
        }

        void Update()
        {
            SetSprite();    
        }

        void SetSprite()
        {
            image.sprite = Persistent.Environment.instance.GetCurrentDreyever().sprite;
        }
    }
}
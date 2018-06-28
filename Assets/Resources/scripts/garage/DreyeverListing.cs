using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Garage
{
    public class DreyeverListing : MonoBehaviour
    {
        public GameObject listImagePrefab;

        void Start()
        {
            foreach(Scriptables.Dreyevers.Dreyever dreyever in Persistent.Environment.instance.allDreyevers) {
                GameObject newObject = Instantiate(listImagePrefab, transform);
                Image image = newObject.GetComponent<Image>();
                image.sprite = dreyever.sprite;
                image.preserveAspect = true;
            }
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Garage
{
    public class DreyeverListing : MonoBehaviour
    {
        public GameObject listImagePrefab;
        public int numberOfColumns;

        void Start()
        {
            foreach(Scriptables.Dreyevers.Dreyever dreyever in Persistent.Environment.instance.allDreyevers) {
                GameObject newObject = Instantiate(listImagePrefab, transform);
                Image image = newObject.GetComponent<Image>();
                image.sprite = dreyever.sprite;
                image.preserveAspect = true;
            }

            StartCoroutine(LayoutGrid());
        }

        IEnumerator LayoutGrid()
        {
            // Only at the end of the frame the RectTransform properties are set.
            yield return new WaitForEndOfFrame();

            RectTransform rectTransform = GetComponent<RectTransform>();
            Rect rect = RectTransformUtility.PixelAdjustRect(rectTransform, GetComponentInParent<Canvas>());

            GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
            float cellSize = (rect.width - grid.spacing.x * (numberOfColumns - 1)) / numberOfColumns;
            grid.cellSize = new Vector2(cellSize, cellSize);
        }
    }
}
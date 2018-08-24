using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Parallax : MonoBehaviour {

    private new SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        Bounds totalBounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach(GameObject gameObject in gameObjects)
        {
            SpriteRenderer gameObjectRenderer = gameObject.GetComponent<SpriteRenderer>();
            if(gameObjectRenderer != null)
            {
                totalBounds.Encapsulate(new Bounds(gameObjectRenderer.bounds.center, gameObjectRenderer.bounds.size));
            } else
            {
                Tilemap gameObjectTilemap = gameObject.GetComponentInChildren<Tilemap>();
                if(gameObjectTilemap != null)
                {
                    gameObjectTilemap.CompressBounds();

                    Bounds bounds = gameObjectTilemap.localBounds;
                    totalBounds.Encapsulate(gameObjectTilemap.transform.TransformPoint(bounds.min));
                    totalBounds.Encapsulate(gameObjectTilemap.transform.TransformPoint(bounds.max));
                }
            }
        }

        float scaleToScreenFit = totalBounds.size.y / renderer.bounds.size.y;
        transform.localScale = new Vector3(transform.localScale.x * scaleToScreenFit,
                transform.localScale.y * scaleToScreenFit, 1);
    }

    void Update()
    {

    }
}

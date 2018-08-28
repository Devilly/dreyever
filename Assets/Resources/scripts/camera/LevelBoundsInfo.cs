using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBoundsInfo : MonoBehaviour {

    public Bounds totalBounds;
    public Bounds environmentalBounds;

    void Start () {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        totalBounds = new Bounds(Vector3.zero, Vector3.zero);
        environmentalBounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach (GameObject gameObject in gameObjects)
        {
            bool isEnvironmental = gameObject.layer == 8;

            SpriteRenderer gameObjectRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (gameObjectRenderer != null)
            {
                totalBounds.Encapsulate(new Bounds(gameObjectRenderer.bounds.center, gameObjectRenderer.bounds.size));

                if (isEnvironmental)
                {
                    environmentalBounds.Encapsulate(new Bounds(gameObjectRenderer.bounds.center, gameObjectRenderer.bounds.size));
                }
            }
            else
            {
                Tilemap gameObjectTilemap = gameObject.GetComponentInChildren<Tilemap>();
                if (gameObjectTilemap != null)
                {
                    gameObjectTilemap.CompressBounds();
                    Bounds bounds = gameObjectTilemap.localBounds;
                    Vector3 minimumVector = gameObjectTilemap.transform.TransformPoint(bounds.min);
                    Vector3 maximumVector = gameObjectTilemap.transform.TransformPoint(bounds.max);

                    totalBounds.Encapsulate(minimumVector);
                    totalBounds.Encapsulate(maximumVector);

                    if (isEnvironmental)
                    {
                        environmentalBounds.Encapsulate(minimumVector);
                        environmentalBounds.Encapsulate(maximumVector);
                    }
                }
            }
        }
    }
}

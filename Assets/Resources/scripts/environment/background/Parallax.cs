using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Parallax : MonoBehaviour {

    public Sprite sprite;
    private GameObject spriteObject;
    private SpriteRenderer spriteRenderer;

    private LevelBoundsInfo levelBoundsInfo;
    
    private float cameraToRendererMovementRatio;

    void Start()
    {
        levelBoundsInfo = Camera.main.GetComponent<LevelBoundsInfo>();

        spriteObject = new GameObject();
        spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        float singleSideMovementSpaceCamera = levelBoundsInfo.totalBounds.extents.y - Camera.main.orthographicSize;
        float singleSideMovementSpaceRenderer = levelBoundsInfo.totalBounds.extents.y - spriteRenderer.bounds.extents.y;
        cameraToRendererMovementRatio = singleSideMovementSpaceRenderer / singleSideMovementSpaceCamera;

        PlaceAccordingToCameraPosition();
    }

    void Update()
    {
        PlaceAccordingToCameraPosition();
    }

    private void PlaceAccordingToCameraPosition()
    {
        float verticalCameraOffset = Camera.main.transform.position.y - levelBoundsInfo.totalBounds.center.y;
        spriteObject.transform.position = new Vector3(transform.position.x, verticalCameraOffset * cameraToRendererMovementRatio, transform.position.z);
    }
}

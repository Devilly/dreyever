using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallOfDreyevers : MonoBehaviour {

    public Sprite[] dreyevers;
    public Sprite overlayedColor;
    [Range(0, 1)]
    public float opacityForOverlayedColor;

    public int numberOfRows;
    public int numberOfColumns;
    
	void Start () {
        float height = Camera.main.orthographicSize * 2;
        float rowHeight = height / (numberOfRows + 1);

        float width = height / Screen.height * Screen.width;
        float columnWidth = width / (numberOfColumns + 1);

        Vector3 topLeftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 bottomRightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        System.Random random = new System.Random();

        for(int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
        {
            for(int columnIndex = 0; columnIndex < numberOfColumns; columnIndex++)
            {
                GameObject dreyeverShadow = new GameObject();
                SpriteMask mask = dreyeverShadow.AddComponent<SpriteMask>();
                mask.sprite = dreyevers[random.Next(0, dreyevers.Length)];
                float resizeBy = 2 / (new float[] { mask.bounds.size.x, mask.bounds.size.y }).Max();
                dreyeverShadow.transform.localScale = new Vector3(dreyeverShadow.transform.localScale.x * resizeBy,
                    dreyeverShadow.transform.localScale.y * resizeBy, 1);
                dreyeverShadow.transform.position = new Vector3(topLeftCorner.x + (columnIndex + 1) * columnWidth,
                    topLeftCorner.y - (rowIndex + 1) * rowHeight, 0);
                // correct position by adapting to changed pivot points

                
            }
        }

        GameObject maskedOverlay = new GameObject();
        SpriteRenderer renderer = maskedOverlay.AddComponent<SpriteRenderer>();
        renderer.sprite = overlayedColor;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, opacityForOverlayedColor);
        renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        maskedOverlay.transform.localScale = new Vector3((bottomRightCorner.x - topLeftCorner.x) / renderer.bounds.size.x,
            (topLeftCorner.y - bottomRightCorner.y) / renderer.bounds.size.y, 1);
        maskedOverlay.transform.position = new Vector3((bottomRightCorner.x + topLeftCorner.x) / 2,
            (bottomRightCorner.y + topLeftCorner.y) / 2, 0);
    }
}

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
        float rowHeight = height / (numberOfRows + .5f);

        float width = height / Screen.height * Screen.width;
        float columnWidth = width / (numberOfColumns + .5f);

        Vector3 topLeftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 bottomRightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        float worldToPixelRatio = Screen.width / (bottomRightCorner.x - topLeftCorner.x);

        System.Random random = new System.Random();

        for(int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
        {
            for(int columnIndex = 0; columnIndex < numberOfColumns; columnIndex++)
            {
                GameObject dreyeverShadow = new GameObject();
                SpriteMask mask = dreyeverShadow.AddComponent<SpriteMask>();
                mask.sprite = dreyevers[random.Next(0, dreyevers.Length)];
                float resizeBy = columnWidth / (new float[] { mask.bounds.size.x, mask.bounds.size.y }).Max();
                dreyeverShadow.transform.localScale = new Vector3(dreyeverShadow.transform.localScale.x * resizeBy,
                    dreyeverShadow.transform.localScale.y * resizeBy, 1);

                float centerX = mask.sprite.rect.width / 2;
                float pivotX = mask.sprite.pivot.x;
                float offsetX = (pivotX - centerX) / mask.sprite.rect.width;

                float centerY = mask.sprite.rect.height / 2;
                float pivotY = mask.sprite.pivot.y;
                float offsetY = (pivotY - centerY) / mask.sprite.rect.height;

                float marginAsCellSizeFraction = .75f;
                dreyeverShadow.transform.position = new Vector3(
                    topLeftCorner.x + (columnIndex + marginAsCellSizeFraction) * columnWidth + mask.bounds.size.x * offsetX,
                    topLeftCorner.y - (rowIndex + marginAsCellSizeFraction) * rowHeight + mask.bounds.size.y * offsetY,
                    0);                
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

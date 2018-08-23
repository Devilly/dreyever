using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallOfDreyevers : MonoBehaviour {

    public Sprite[] dreyevers;

    public Material spriteMaterial;

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
                SpriteRenderer spriteRenderer = dreyeverShadow.AddComponent<SpriteRenderer>();
                spriteRenderer.material = spriteMaterial;
                spriteRenderer.sprite = dreyevers[random.Next(0, dreyevers.Length)];
                float resizeBy = columnWidth / (new float[] { spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y }).Max();
                dreyeverShadow.transform.localScale = new Vector3(dreyeverShadow.transform.localScale.x * resizeBy,
                    dreyeverShadow.transform.localScale.y * resizeBy, 1);

                float centerX = spriteRenderer.sprite.rect.width / 2;
                float pivotX = spriteRenderer.sprite.pivot.x;
                float offsetX = (pivotX - centerX) / spriteRenderer.sprite.rect.width;

                float centerY = spriteRenderer.sprite.rect.height / 2;
                float pivotY = spriteRenderer.sprite.pivot.y;
                float offsetY = (pivotY - centerY) / spriteRenderer.sprite.rect.height;

                float marginAsCellSizeFraction = .75f;
                dreyeverShadow.transform.position = new Vector3(
                    topLeftCorner.x + (columnIndex + marginAsCellSizeFraction) * columnWidth + spriteRenderer.bounds.size.x * offsetX,
                    topLeftCorner.y - (rowIndex + marginAsCellSizeFraction) * rowHeight + spriteRenderer.bounds.size.y * offsetY,
                    0);                
            }
        }
    }
}

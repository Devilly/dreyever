using UnityEngine;

public class HandleTouches : MonoBehaviour {
	
	void Update () {
        if (Input.touches.Length == 0) return;

        Touch touch = Input.GetTouch(0);
        Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

        if (hit.collider == null) return;

        hit.collider.SendMessage("ihavebeentouched", null, SendMessageOptions.DontRequireReceiver);
    }
}

using UnityEngine;

namespace CameraSpace {
	public class Background : MonoBehaviour {

		private Camera component;

        public Color backgroundColor = new Color(14, 16, 18, 255);

		void Start () {
            component = GetComponent<Camera>();
		}

        private void Update()
        {
            component.backgroundColor = backgroundColor;
        }
	}
}
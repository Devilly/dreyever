using UnityEngine;

namespace CameraSpace {
	public class Background : MonoBehaviour {

		private Camera component;
        private bool arAvailable = false;
        public int minColorValue = 251;
        public int maxColorValue = 251;

		void Start () {
            component = GetComponent<Camera>();
		}

        private void Update()
        {
            float pixelIntensity = 1;
            float colorValue = minColorValue + (maxColorValue - minColorValue) * pixelIntensity;
            component.backgroundColor = new Color(
                convertColorIntToFloat(colorValue),
                convertColorIntToFloat(colorValue),
                convertColorIntToFloat(colorValue)
            );
        }

        private float convertColorIntToFloat(float color) {
			return 1f / 255 * color;
		}
	}
}
using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CameraSpace {
	public class Background : MonoBehaviour {

		private Camera component;
        private bool arAvailable = false;
        public int minColorValue = 251;
        public int maxColorValue = 251;

		void Start () {
            component = GetComponent<Camera>();

            AsyncTask<GoogleARCore.ApkAvailabilityStatus> arAvailability = Session.CheckApkAvailability();
            arAvailability.ThenAction(status =>
            {
                arAvailable = status == ApkAvailabilityStatus.SupportedInstalled;
            });
		}

        private void Update()
        {
            float pixelIntensity = 1;
            if (arAvailable) pixelIntensity = Frame.LightEstimate.PixelIntensity * 5;

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
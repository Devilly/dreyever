using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FromPortraitToLandscape
{
    public class LetPlayerChangeOrientation : MonoBehaviour
    {
        public string targetScene;
        private bool loadingScene = false;

        public GameObject redEye;
        private Image redEyeImage;
        private Image redEyeText;

        public GameObject greenEye;
        private Image greenEyeImage;
        private Image greenEyeText;

        private const float xGravityTarget = -.5f;
        private const float xGravityBuffer = .05f;

        void Awake()
        {
            Input.gyro.enabled = true;
            CheckIfReadyAndAct();
        }

        void Start()
        {
            if (loadingScene) return;

            redEyeImage = redEye.GetComponent<Image>();
            redEyeText = redEye.transform.Find("RedEyeText").GetComponent<Image>();

            greenEyeImage = greenEye.GetComponent<Image>();
            greenEyeText = greenEye.transform.Find("GreenEyeText").GetComponent<Image>();
        }

        void Update()
        {
            if (loadingScene) return;

            float xGravityCurrent = Input.gyro.gravity.x;
            float yGravityCurrent = Input.gyro.gravity.y;

            float fractionToWantedRotation;
            if(xGravityCurrent >= 0)
            {
                fractionToWantedRotation = 0;
            } else
            {
                fractionToWantedRotation = xGravityCurrent / xGravityTarget;
            }
            
            redEyeImage.color = new Color(1, 1, 1, 1 - fractionToWantedRotation);
            redEyeText.color = new Color(1, 1, 1, 1 - fractionToWantedRotation);

            greenEyeImage.color = new Color(1, 1, 1, fractionToWantedRotation);
            greenEyeText.color = new Color(1, 1, 1, fractionToWantedRotation);

            float zRotation;
            if (yGravityCurrent <= 0)
            {
                zRotation = -90 * fractionToWantedRotation;
            } else
            {
                zRotation = -90 + -90 * (1 - fractionToWantedRotation);
            }
            
            redEye.transform.rotation = Quaternion.Euler(0, 0, zRotation);
            greenEye.transform.rotation = Quaternion.Euler(0, 0, zRotation);

            CheckIfReadyAndAct();
        }

        private void CheckIfReadyAndAct()
        {
            var xGravityCurrent = Input.gyro.gravity.x;
            if (xGravityCurrent > xGravityTarget - xGravityBuffer &&
                xGravityCurrent < xGravityTarget + xGravityBuffer)
            {
                StartCoroutine(StartGame());
            }
            
        }

        private IEnumerator StartGame()
        {
            loadingScene = true;
            yield return new WaitForSeconds(.5f);
            
            SceneManager.LoadScene(targetScene);
        }
    }
}
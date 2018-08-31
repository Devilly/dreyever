using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Util
{
    public abstract class DoSomethingAfterTouchAndAnimation : DoSomethingAfterTouch
    {
        private SpriteRenderer spriteRenderer;
        public Sprite[] animationSprites;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void StartSomethingAfterTouch()
        {
            StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            float frameTime = 1f / 40;
            foreach (Sprite sprite in animationSprites)
            {
                yield return new WaitForSeconds(frameTime);
                spriteRenderer.sprite = sprite;
            }

            StartSomethingAfterTouchAndAnimation();
        }

        public abstract void StartSomethingAfterTouchAndAnimation();
    }
}
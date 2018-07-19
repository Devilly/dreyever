using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreyever
{
    public class Animator : MonoBehaviour
    {
        private Animation currentAnimation;
        private float animationStart;

        public int fps = 30;
        private float millisPerFrame;
        
        void Start()
        {
            currentAnimation = Animation.NONE;

            millisPerFrame = 1f / fps;
        }

        public void StartAnimation(Animation animation)
        {
            if(animation != currentAnimation)
            {
                currentAnimation = animation;
                animationStart = Time.time;
            }
        }
        
        void Update()
        {
            if (currentAnimation == Animation.NONE) return;

            Sprite[] sprites;
            if (currentAnimation == Animation.TILT)
            {
                sprites = Persistent.Environment.instance.GetCurrentDreyever().tilt;
            }
            else if (currentAnimation == Animation.JUMP)
            {
                sprites = Persistent.Environment.instance.GetCurrentDreyever().jump;
            }
            else if (currentAnimation == Animation.AIRTURN)
            {
                sprites = Persistent.Environment.instance.GetCurrentDreyever().airturn;
            }
            else if (currentAnimation == Animation.LANDING)
            {
                sprites = Persistent.Environment.instance.GetCurrentDreyever().landing;
            } else
            {
                throw new NotImplementedException();
            }

            var frameIndex = (int) ((Time.time - animationStart) / millisPerFrame);
            frameIndex = Mathf.Clamp(frameIndex, 0, sprites.Length - 1);

            GetComponent<SpriteRenderer>().sprite = sprites[frameIndex];
        }
    }
}
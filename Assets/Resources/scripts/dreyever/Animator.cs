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
        private Direction currentDirection;

        public int fps = 40;
        private float millisPerFrame;

        public delegate void Callback();
        private Callback callback;
        
        void Start()
        {
            currentAnimation = Animation.NONE;

            millisPerFrame = 1f / fps;
        }

        public void StartAnimation(Animation animation, Direction direction)
        {
            StartAnimation(animation, direction, () => { });
        }

        public void StartAnimation(Animation animation, Direction direction, Callback callback)
        {
            if(direction == currentDirection)
            {
                if ((animation == Animation.TILT) && (currentAnimation != Animation.NONE))
                {
                    return;
                }
            }

            currentAnimation = animation;
            animationStart = Time.time;
            currentDirection = direction;

            this.callback = callback;
        }
        
        void Update()
        {
            if (currentAnimation == Animation.NONE) return;

            Sprite[] sprites;
            if (currentAnimation == Animation.TILT)
            {
                if(currentDirection == Direction.RIGHT)
                {
                    sprites = Persistent.Environment.instance.GetCurrentDreyever().tilt;
                } else
                {
                    sprites = Persistent.Environment.instance.GetCurrentDreyever().reverseTilt;
                }
            }
            else if (currentAnimation == Animation.AIRTURN)
            {
                if (currentDirection == Direction.RIGHT)
                {
                    sprites = Persistent.Environment.instance.GetCurrentDreyever().airturn;
                }
                else
                {
                    sprites = Persistent.Environment.instance.GetCurrentDreyever().reverseAirturn;
                }
            }
            else if (currentAnimation == Animation.LANDING)
            {
                if (currentDirection == Direction.RIGHT)
                {
                    sprites = Persistent.Environment.instance.GetCurrentDreyever().landing;
                }
                else
                {
                    sprites = Persistent.Environment.instance.GetCurrentDreyever().reverseLanding;
                }
            }
            else if (currentAnimation == Animation.EXPLOSION)
            {
                // After an explosion there is nothing.
                // Adding a null entry in the array to represent the "nothing".
                // Would be the same to add an empty sprite but this is less resource intensive.
                Sprite[] arrayWithTrailingNull = new Sprite[Persistent.Environment.instance.GetCurrentDreyever().explosion.Length + 1];
                Array.Copy(Persistent.Environment.instance.GetCurrentDreyever().explosion, arrayWithTrailingNull, Persistent.Environment.instance.GetCurrentDreyever().explosion.Length);
                sprites = arrayWithTrailingNull;
            }
            else
            {
                throw new NotImplementedException();
            }

            var frameIndex = (int) ((Time.time - animationStart) / millisPerFrame);
            frameIndex = Mathf.Clamp(frameIndex, 0, sprites.Length - 1);

            GetComponent<SpriteRenderer>().sprite = sprites[frameIndex];

            if (frameIndex == sprites.Length - 1)
            {
                if (this.callback != null)
                {
                    this.callback();
                    this.callback = null;
                }

                if(currentAnimation != Animation.TILT)
                {
                    currentAnimation = Animation.NONE;
                }
            }
        }
    }
}
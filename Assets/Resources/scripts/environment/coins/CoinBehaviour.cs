using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coins
{
    public class CoinBehaviour : MonoBehaviour
    {
        private Animator animator;

        public CoinType type;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "dreyever")
            {
                animator.Play("collect");
                Persistent.Environment.instance.IncreaseCoinsCount(type);
            }
        }
    }
}
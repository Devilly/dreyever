using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coins
{
    public class CoinBehaviour : MonoBehaviour
    {
        public CoinType type;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.name == "Hitbox")
            {
                Destroy(gameObject);
                Persistent.Environment.instance.IncreaseCoinsCount(type);
            }
        }
    }
}
using Coins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class TextCoinsCount : MonoBehaviour
    {
        public CoinType type;

        void Start()
        {
            GetComponent<Text>().text = Persistent.Environment.instance.GetCoinsCount(type).ToString();
        }
    }
}
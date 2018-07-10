using Garage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Persistent.Model
{
    [System.Serializable]
    public class Progress
    {
        public string currentDreyever;

        public string[] unlockedDreyevers;

        public int coinsCountA;
        public int coinsCountB;
        public int coinsCountC;
    }
}
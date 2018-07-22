﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Dreyevers
{
    [CreateAssetMenu(fileName ="Dreyever", menuName ="Dreyever")]
    public class Dreyever : ScriptableObject
    {
        public new string name;
        public string displayName;
        public Sprite sprite;
        public Sprite blackSprite;

        public Sprite[] tilt;
        public Sprite[] airturn;
        public Sprite[] landing;

        public Sprite[] explosion;
    }
}
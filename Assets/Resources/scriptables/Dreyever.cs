using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName ="Dreyever", menuName ="Dreyever")]
    public class Dreyever : ScriptableObject
    {
        public new string name;
        public string displayName;
        public Sprite sprite;
    }
}
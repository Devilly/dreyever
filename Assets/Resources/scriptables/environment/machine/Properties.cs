using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Environment.Machine
{
    [CreateAssetMenu(fileName = "Machine properties", menuName = "Machine properties")]
    public class Properties : ScriptableObject
    {
        public Sprite sprite;
        public Sprite mirroredSprite;

        public readonly Vector3 roofPosition = new Vector3(.06f, .92f, 0);
        public readonly Vector3 mirroredRoofPosition = new Vector3(-.051f, .918f, 0);

        public readonly Vector3 symbolPosition = new Vector3(.582f, -.429f, 0);
        public readonly Vector3 mirroredSymbolPosition = new Vector3(-.574f, -.429f, 0);

        public readonly Vector3 executorPosition = new Vector3(1.01f, -.02f, 0);
        public readonly Vector3 mirroredExecutorPosition = new Vector3(-1, -.01f, 0);
    }
}
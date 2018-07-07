using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Util
{
    [CreateAssetMenu(fileName = "Instantiater", menuName = "Instantiater")]
    public class Instantiater : ScriptableObject
    {
        public GameObject objectToClone;

        public GameObject Clone()
        {
            return Instantiate(objectToClone);
        }
    }
}
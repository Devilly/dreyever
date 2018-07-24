using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Factory
{
    public class Behaviour : MonoBehaviour
    {
        public GameObject dreyeverPrefab;
        public Sprite[] animationSprites;
        
        void Start()
        {
            Instantiate(dreyeverPrefab, transform.position, Quaternion.identity);
        }
    }
}
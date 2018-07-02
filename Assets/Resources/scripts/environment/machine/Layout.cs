using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Machine
{
    [ExecuteInEditMode]
    public class Layout : MonoBehaviour
    {
        public bool mirrored;
        public Scriptables.Environment.Machine.Properties properties;

        void Update()
        {
            if(!Application.isPlaying)
            {
                GetComponent<SpriteRenderer>().sprite = mirrored ? properties.mirroredSprite : properties.sprite;
                transform.Find("Roof").transform.localPosition = mirrored ? properties.mirroredRoofPosition : properties.roofPosition;
                transform.Find("Symbol").transform.localPosition = mirrored ? properties.mirroredSymbolPosition : properties.symbolPosition;
                transform.Find("Executor").transform.localPosition = mirrored ? properties.mirroredExecutorPosition : properties.executorPosition;
            }
        }
    }
}
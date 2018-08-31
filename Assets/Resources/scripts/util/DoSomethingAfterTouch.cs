using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public abstract class DoSomethingAfterTouch : MonoBehaviour
    {
        private bool activated = false;

        public void ihavebeentouched()
        {
            if (activated) return;
            else activated = true;

            StartSomethingAfterTouch();
            StartCoroutine(ResetActivation());
        }

        public abstract void StartSomethingAfterTouch();

        private IEnumerator ResetActivation()
        {
            yield return new WaitForSeconds(3);
            activated = false;
        }
    }
}
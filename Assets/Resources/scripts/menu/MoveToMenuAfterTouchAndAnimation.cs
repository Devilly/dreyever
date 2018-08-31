using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Menu
{
    public class MoveToMenuAfterTouchAndAnimation : DoSomethingAfterTouchAndAnimation
    {
        public override void StartSomethingAfterTouchAndAnimation()
        {
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(0);
            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
    }
}
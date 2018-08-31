using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Menu
{
    public class MoveToGarageAfterTouchAndAnimation : DoSomethingAfterTouchAndAnimation
    {
        public override void StartSomethingAfterTouchAndAnimation()
        {
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(1);
            Camera.main.transform.position = new Vector3(14, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
    }
}
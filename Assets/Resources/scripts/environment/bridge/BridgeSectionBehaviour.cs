using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Bridge
{
    public class BridgeSectionBehaviour : MonoBehaviour
    {
        public BridgePartInteract[] bridgePartInteracts;

        private void Start()
        {
            foreach(BridgePartInteract bridgePartInteract in bridgePartInteracts)
            {
                bridgePartInteract.SetBridgeSectionBehaviour(this);
                bridgePartInteract.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
        }

        public void HandleInteract()
        {
            StartCoroutine(DropAfterPeriodOfLoad());
        }

        private IEnumerator DropAfterPeriodOfLoad()
        {
            yield return new WaitForSeconds(.25f);

            foreach (BridgePartInteract bridgePartInteract in bridgePartInteracts)
            {
                //bridgePartInteract.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Bridge
{
    public class BridgePartInteract : InteractionBase
    {
        private BridgeSectionBehaviour bridgeSectionBehaviour;

        public void SetBridgeSectionBehaviour(BridgeSectionBehaviour bridgeSectionBehaviour)
        {
            this.bridgeSectionBehaviour = bridgeSectionBehaviour;
        }

        public override Influence Do()
        {
            bridgeSectionBehaviour.HandleInteract();

            return new Influence();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : StateMachineBehaviour {

    private SpriteRenderer renderer;
    private bool hasReachedTop = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        renderer = animator.gameObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);

        hasReachedTop = false;
	}
    
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        const float alphaTop = 0.5f;
        const int framesToTop = 4;
        const int totalNumberOfFrames = 15;

        float alphaDifference;
        if(!hasReachedTop && renderer.color.a < alphaTop)
        {
            alphaDifference = alphaTop / framesToTop;
        } else
        {
            alphaDifference = alphaTop / (totalNumberOfFrames - framesToTop) * -1;
        }

        float newAlpha = renderer.color.a + alphaDifference;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, newAlpha);

        if (newAlpha >= alphaTop)
        {
            hasReachedTop = true;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : StateMachineBehaviour {

    private SpriteRenderer renderer;
    private bool hasReachedTop = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        renderer = animator.gameObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
	}
    
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        const float alphaTop = 0.5f;

        float alphaDifference;
        if(!hasReachedTop && renderer.color.a < alphaTop)
        {
            alphaDifference = alphaTop / 10;
        } else
        {
            alphaDifference = alphaTop / 10 * -1;
        }

        float newAlpha = renderer.color.a + alphaDifference;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, newAlpha);

        if (newAlpha >= alphaTop)
        {
            hasReachedTop = true;
        }
    }
}

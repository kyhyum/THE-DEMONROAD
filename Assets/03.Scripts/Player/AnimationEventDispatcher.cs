using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnityAnimationEvent : UnityEvent<string> { };
[RequireComponent(typeof(Animator))]

public class AnimationEventDispatcher : MonoBehaviour
{
    public Player player;

    public UnityAnimationEvent OnAnimationStart;
    public UnityAnimationEvent OnAnimationComplete;

    Animator animator;

    public AnimationEventDispatcher(Player player)
    {
        this.player = player;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];

            AnimationEvent animationStartEvent = new AnimationEvent();
            animationStartEvent.time = 0f;
            animationStartEvent.functionName = "AnimationStartHandler";
            animationStartEvent.stringParameter = clip.name;

            AnimationEvent animationEndEvent = new AnimationEvent();
            animationEndEvent.time = clip.length;
            animationEndEvent.functionName = "AnimationCompleteHandler";
            animationEndEvent.stringParameter = clip.name;

            clip.AddEvent(animationStartEvent);
            clip.AddEvent(animationEndEvent);
        }
    }

    public void AnimationStartHandler(string name)
    {
        Debug.Log($"{name} 애니메이션 시작한다.");

        if (name == "Standing Melee Attack Downward")
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            {
                player.TrailRenderer.enabled = true;
            }
        }

        OnAnimationStart?.Invoke(name);
    }

    public void AnimationCompleteHandler(string name)
    {
        Debug.Log($"{name} 애니메이션 끝났다.");

        if (name == "Standing Melee Attack Downward")
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                player.TrailRenderer.enabled = false;
            }
        }
        
        OnAnimationComplete?.Invoke(name);
    }
}

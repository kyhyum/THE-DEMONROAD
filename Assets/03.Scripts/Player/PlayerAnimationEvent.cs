using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [field: SerializeField] private TrailRenderer trailRenderer;

    public void TrailRendererEnabled()
    {
        Debug.Log("PlayerAnimationEvent 클래스의 TrailRendererEnabled 함수 호출했다.");

        trailRenderer.enabled = true;
    }

    public void TrailRendererDisabled()
    {
        Debug.Log("PlayerAnimationEvent 클래스의 TrailRendererDisabled 함수 호출했다.");

        trailRenderer.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodgeState : PlayerGroundedState
{
    public PlayerDodgeState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(GameManager.Instance.player.AnimationData.DodgeParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(GameManager.Instance.player.AnimationData.DodgeParameterHash);
        GameManager.Instance.player.playerCollider.enabled = true;
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(GameManager.Instance.player.Animator, "Dodge");
        Debug.Log(normalizedTime);
        if(normalizedTime < 1f)
        {
            GameManager.Instance.player.playerCollider.enabled = false;
        }
        else if(normalizedTime > 1f)
        {
            GameManager.Instance.player.Agent.ResetPath();
            OnStand();
        }
        
    }

    protected override void OnMove()
    {

    }

    protected override void OnMoveStarted(InputAction.CallbackContext context)
    {
    }

    protected override void OnMovePerformed(InputAction.CallbackContext context)
    {
    }

    protected override void OnMoveCanceled(InputAction.CallbackContext context)
    {
    }
}

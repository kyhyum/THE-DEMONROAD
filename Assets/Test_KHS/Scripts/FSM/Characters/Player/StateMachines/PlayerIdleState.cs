using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;

        base.Enter();

        //StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        //StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //if (stateMachine.MovementInput != Vector2.zero)
        //{
        //    OnMove();
        //    return;
        //}

        stateMachine.Speed = stateMachine.Player.Agent.velocity.magnitude;
        Debug.Log($"stateMachine.Speed : {stateMachine.Speed}");
        if (stateMachine.Speed != 0)
        {
            OnMove();
            return;
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput != Vector2.zero)
        {
            return;
        }

        //stateMachine.ChangeState(stateMachine.IdleState);

        base.OnMovementCanceled(context);
    }

    //protected virtual void OnMove()
    private void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }
}

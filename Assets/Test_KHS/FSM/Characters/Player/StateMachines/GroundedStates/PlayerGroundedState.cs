using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.IsAttacking)
        {
            OnAttack();
            return;
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    //
    protected override void OnMoveStarted(InputAction.CallbackContext context)
    {
        // 입력 처리가 안들어오면 
        if (stateMachine.Player.Agent.velocity != Vector3.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.IdleState);

        base.OnMoveStarted(context);
    }

    protected virtual void OnStand()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    protected virtual void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.AttackState);
    }
}

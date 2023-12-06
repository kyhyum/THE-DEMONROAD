using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(GameManager.Instance.player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(GameManager.Instance.player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.Instance.player.IsDodging)
        {
            OnDodge();
        }
        else if (GameManager.Instance.player.IsAttacking)
        {
            OnAttack();
            return;
        }
        else if (GameManager.Instance.player.IsAttackSkill[0])
        {
            OnAttackSkill1();
            return;
        }
        else if (GameManager.Instance.player.IsAttackSkill[1])
        {
            OnAttackSkill2();
            return;
        }
        else if (GameManager.Instance.player.IsAttackSkill[2])
        {
            OnAttackSkill3();
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

    protected override void OnMoveStarted(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.player.Agent.velocity != Vector3.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.IdleState);

        base.OnMoveStarted(context);
    }

    protected override void OnMovePerformed(InputAction.CallbackContext context)
    {

        if (GameManager.Instance.player.Agent.velocity != Vector3.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.IdleState);

        base.OnMovePerformed(context);
    }

    protected override void OnMoveCanceled(InputAction.CallbackContext context)
    {
        base.OnMoveCanceled(context);
    }

    protected virtual void OnStand()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    protected virtual void OnDodge()
    {
        stateMachine.ChangeState(stateMachine.DodgeState);
    }

    protected virtual void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.ComboAttackState);
    }

    protected virtual void OnAttackSkill1()
    {
        stateMachine.ChangeState(stateMachine.AttackSkill1State);
    }

    protected virtual void OnAttackSkill2()
    {
        stateMachine.ChangeState(stateMachine.AttackSkill2State);
    }

    protected virtual void OnAttackSkill3()
    {
        stateMachine.ChangeState(stateMachine.AttackSkill3State);
    }
}

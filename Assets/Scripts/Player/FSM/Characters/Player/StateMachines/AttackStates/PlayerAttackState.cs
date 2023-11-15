using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        // 공격할 때 이동을 못하게 한다. 
        stateMachine.MovementSpeedModifier = 0;
        // agent.ResetPath();
        stateMachine.Player.Agent.ResetPath();
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 0.8f)
        {
            if (stateMachine.Player.IsAttacking)
            {

            }
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSkill2State : PlayerBaseState
{
    public PlayerAttackSkill2State(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.Agent.ResetPath();
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackSkill2ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackSkill2ParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "AttackSkill2");
        if (normalizedTime < 0.8f)
        {
            if (stateMachine.Player.IsAttackSkill2)
            {

            }
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

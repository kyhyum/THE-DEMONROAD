using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knight - Strike
public class PlayerAttackSkill1State : PlayerBaseState
{
    public PlayerAttackSkill1State(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        KnightSkill knightSkill = stateMachine.Player.KnightSkill;
        knightSkill.Use(knightSkill.strikeSkillSO, 1);

        stateMachine.Player.Agent.ResetPath();
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackSkill1ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackSkill1ParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "AttackSkill1");
        if (normalizedTime < 0.8f)
        {
            if (stateMachine.Player.IsAttackSkill1)
            {

            }
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

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
        //KnightSkill knightSkill = stateMachine.Player.KnightSkill;
        //knightSkill.Use(knightSkill.shieldStrikeSO, 1);

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
            if (stateMachine.Player.IsAttackSkill[1])
            {
                stateMachine.Player.skillRange[1].gameObject.SetActive(true);
            }
        }
        else
        {
            stateMachine.Player.skillRange[1].gameObject.SetActive(false);
            stateMachine.Player.IsAttackSkill[1] = false;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

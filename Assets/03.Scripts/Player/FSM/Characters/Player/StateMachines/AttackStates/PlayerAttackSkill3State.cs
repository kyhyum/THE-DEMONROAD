using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSkill3State : PlayerBaseState
{
    public PlayerAttackSkill3State(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.WeaponCollider.enabled = true;

        stateMachine.Player.animationEventEffects.SetEffects(stateMachine.Player.playerSkill3Data.Effects);

        stateMachine.Player.Agent.ResetPath();
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackSkill3ParameterHash);
    }

    public override void Exit()
    {
        stateMachine.Player.WeaponCollider.enabled = false;

        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackSkill3ParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "AttackSkill3");
        if (normalizedTime < 0.8f)
        {
            if (stateMachine.Player.IsAttackSkill[2])
            {
                stateMachine.Player.skillRange[2].gameObject.SetActive(true);
            }
        }
        else
        {
            stateMachine.Player.skillRange[2].gameObject.SetActive(false);
            stateMachine.Player.IsAttackSkill[2] = false;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

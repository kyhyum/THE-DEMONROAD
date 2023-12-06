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
        GameManager.Instance.player.animationEventEffects.SetEffects(GameManager.Instance.player.playerSkill3Data.Effects);

        GameManager.Instance.player.Agent.ResetPath();
        base.Enter();

        StartAnimation(GameManager.Instance.player.AnimationData.AttackSkill3ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(GameManager.Instance.player.AnimationData.AttackSkill3ParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(GameManager.Instance.player.Animator, "AttackSkill3");
        if (normalizedTime < 0.8f)
        {
            if (GameManager.Instance.player.IsAttackSkill[2])
            {
                GameManager.Instance.player.skillRange[2].gameObject.SetActive(true);
            }
        }
        else
        {
            GameManager.Instance.player.skillRange[2].gameObject.SetActive(false);
            GameManager.Instance.player.IsAttackSkill[2] = false;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

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
        GameManager.Instance.player.WeaponCollider.enabled = true;

        GameManager.Instance.player.animationEventEffects.SetEffects(GameManager.Instance.player.playerSkill2Data.Effects);
        GameManager.Instance.player.Agent.ResetPath();
        base.Enter();

        StartAnimation(GameManager.Instance.player.AnimationData.AttackSkill2ParameterHash);
    }

    public override void Exit()
    {
        GameManager.Instance.player.WeaponCollider.enabled = false;

        base.Exit();

        StopAnimation(GameManager.Instance.player.AnimationData.AttackSkill2ParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(GameManager.Instance.player.Animator, "AttackSkill2");
        if (normalizedTime < 0.8f)
        {
            if (GameManager.Instance.player.IsAttackSkill[1])
            {
                GameManager.Instance.player.skillRange[1].gameObject.SetActive(true);
            }
        }
        else
        {
            GameManager.Instance.player.skillRange[1].gameObject.SetActive(false);
            GameManager.Instance.player.IsAttackSkill[1] = false;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

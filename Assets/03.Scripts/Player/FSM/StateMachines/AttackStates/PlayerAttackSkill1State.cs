using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Knight - Strike
public class PlayerAttackSkill1State : PlayerBaseState
{
    public PlayerAttackSkill1State(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        GameManager.Instance.player.WeaponCollider.enabled = true;

        GameManager.Instance.player.animationEventEffects.SetEffects(GameManager.Instance.player.playerSkill1Data.Effects);

        GameManager.Instance.player.Agent.ResetPath();
        base.Enter();

        StartAnimation(GameManager.Instance.player.AnimationData.AttackSkill1ParameterHash);
    }

    public override void Exit()
    {
        GameManager.Instance.player.WeaponCollider.enabled = false;

        base.Exit();
        StopAnimation(GameManager.Instance.player.AnimationData.AttackSkill1ParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(GameManager.Instance.player.Animator, "AttackSkill1");

        if (normalizedTime < 0.9f)
        {
            if (GameManager.Instance.player.IsAttackSkill[0])
            {
                GameManager.Instance.player.skillRange[0].gameObject.SetActive(true);
            }
        }
        else
        {
            GameManager.Instance.player.skillRange[0].gameObject.SetActive(false);
            GameManager.Instance.player.IsAttackSkill[0] = false;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

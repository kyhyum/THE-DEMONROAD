using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerGroundedState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        //Debug.Log("PlayerAttackState 클래스 Enter 함수 호출한다.");
        GameManager.Instance.player.WeaponCollider.enabled = true;

        GameManager.Instance.player.Agent.ResetPath();
        GameManager.Instance.player.animationEventEffects.SetEffects(GameManager.Instance.player.playerBaseAttackData.Effects);
        base.Enter();
        StartAnimation(GameManager.Instance.player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        GameManager.Instance.player.WeaponCollider.enabled = false;

        base.Exit();

        StopAnimation(GameManager.Instance.player.AnimationData.AttackParameterHash);
    }


    protected override void OnMove()
    {
    }
}

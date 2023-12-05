using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyApplyCombo;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void Enter()
    {
        GameManager.Instance.player.WeaponCollider.enabled = true;

        base.Enter();
        StartAnimation(GameManager.Instance.player.AnimationData.ComboAttackParameterHash);

        alreadyApplyCombo = false;

        attackInfoData = GameManager.Instance.player.playerSO.AttakData[0].GetAttackInfo(stateMachine.ComboIndex);
        GameManager.Instance.player.Animator.SetInteger("BasicAttackCombo", stateMachine.ComboIndex);
    }

    public override void Exit()
    {
        GameManager.Instance.player.WeaponCollider.enabled = false;

        base.Exit();
        StopAnimation(GameManager.Instance.player.AnimationData.ComboAttackParameterHash);

        if (!alreadyApplyCombo)
            stateMachine.ComboIndex = 0;
    }

    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!GameManager.Instance.player.IsAttacking) return;

        alreadyApplyCombo = true;
    }

    public override void Update()
    {
        base.Update();


        float normalizedTime = GetNormalizedTime(GameManager.Instance.player.Animator, "Attack");
        if (normalizedTime < 1f)
        {

            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack();
        }
        else
        {
            if (alreadyApplyCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}
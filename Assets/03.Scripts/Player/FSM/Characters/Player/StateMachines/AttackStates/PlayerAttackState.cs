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
        //Debug.Log("PlayerAttackState 클래스 Enter 함수 호출한다.");
        
        stateMachine.Player.Agent.ResetPath();
        base.Enter();

        stateMachine.Player.TrailRenderer.enabled = true;
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        stateMachine.Player.TrailRenderer.enabled = false;
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 0.8f)
        {
            if (stateMachine.Player.IsAttacking)
            {
                //Debug.Log("PlayerAttackState 클래스 Update 함수 호출한다.");
            }

            //Debug.Log("PlayerAttackState 클래스 Update2 함수 호출한다. 2");
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

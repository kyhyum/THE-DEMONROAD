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
        Debug.Log("PlayerAttackState 클래스 Enter 함수 호출했다.");

        Weapon weapon = stateMachine.Player.Weapon;

        //stateMachine.Player.AttackSlash
        Object.Instantiate(weapon.AttackSlash, weapon.transform.position, Quaternion.identity);
        
        stateMachine.Player.Agent.ResetPath();
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 0.8f)
        {
            if (stateMachine.Player.IsAttacking)
            {
                Debug.Log("공격한다.");
            }

            Debug.Log("공격한다. 2");
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

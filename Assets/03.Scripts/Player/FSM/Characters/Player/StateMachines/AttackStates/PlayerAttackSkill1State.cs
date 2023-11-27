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

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        KnightSkill knightSkill = stateMachine.Player.KnightSkill;

        Debug.Log("PlayerAttackSkill1State 클래스 OnTriggerEnter 함수 호출한다.");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("적을 공격했다.");

            if (other.TryGetComponent(out Health health))
            {
                Debug.Log($"데미지 {knightSkill.totalDamage}를 입혔다.");

                health.TakeDamage(knightSkill.totalDamage);
            }
        }
    }
}

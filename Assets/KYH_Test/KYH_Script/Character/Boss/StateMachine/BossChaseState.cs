using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class BossChaseState : BossBaseState
{
    public BossChaseState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Boss.bossAnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Boss.bossAnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        base.Update();
        stateMachine.Boss.BossNavMeshAgent.SetDestination(stateMachine.Target.transform.position);
        if (!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.Boss.BossNavMeshAgent.ResetPath();
            //stateMachine.ChangeState(stateMachine.AttackState);
            //TODO: 체력에 비례해 공격 패턴 여러가지 설정
            return;
        }
    }
    private bool IsInAttackRange()
    {
        // if (stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Boss.transform.position).sqrMagnitude;

        //return playerDistanceSqr <= stateMachine.Boss.Data.AttackRange * stateMachine.Boss.Data.AttackRange;
        return false;
    }
}

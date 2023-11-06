using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MonsterChaseState : MonsterBaseState
{
    public MonsterChaseState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        stateMachine = monsterStateMachine;
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1;
        base.Enter();
        StartAnimation(stateMachine.Monster.monsterAnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.monsterAnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        base.Update();
        stateMachine.Monster.EnemyNavMeshAgent.SetDestination(stateMachine.Target.transform.position);
        if (!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.Monster.EnemyNavMeshAgent.ResetPath();
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }
    private bool IsInAttackRange()
    {
        // if (stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Monster.Data.AttackRange * stateMachine.Monster.Data.AttackRange;
    }
}

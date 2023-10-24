using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    public MonsterAttackState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        stateMachine = monsterStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Monster.monsterAnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.monsterAnimationData.WalkParameterHash);
    }
}

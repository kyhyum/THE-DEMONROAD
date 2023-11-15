using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStunState : MonsterBaseState
{
    float elapsedTime = 0;
    float stunTime;
    public MonsterStunState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        stateMachine = monsterStateMachine;
    }
    public override void Enter()
    {
        elapsedTime = 0;
        base.Enter();
        StartAnimation(stateMachine.Monster.monsterAnimationData.StunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.monsterAnimationData.StunParameterHash);
    }

    public override void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= stunTime)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
    }

    public void SetStunTime(float time)
    {
        stunTime = time;
    }
}

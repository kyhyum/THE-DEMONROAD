using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{

    public BossIdleState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Boss.bossAnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Boss.bossAnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        if (IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }

}

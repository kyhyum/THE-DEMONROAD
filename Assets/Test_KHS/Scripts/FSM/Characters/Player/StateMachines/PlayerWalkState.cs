using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();

        stateMachine.Speed = stateMachine.Player.Agent.velocity.magnitude;
        //Debug.Log($"stateMachine.Speed : {stateMachine.Speed}");
        if (stateMachine.Speed == 0)
        {
            OnStop();
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    private void OnStop()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}

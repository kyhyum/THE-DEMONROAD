using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(GameManager.Instance.player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(GameManager.Instance.player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        // 이동이 일어나면
        if (GameManager.Instance.player.Agent.velocity != Vector3.zero)
        {
            OnMove();
            return;
        }
    }
}

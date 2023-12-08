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

        Player player = GameManager.Instance.player;

        StartAnimation(player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        Player player = GameManager.Instance.player;

        StopAnimation(player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        Player player = GameManager.Instance.player;

        // 이동이 일어나면
        if (player.Agent.velocity != Vector3.zero)
        {
            OnMove();
            return;
        }
    }
}

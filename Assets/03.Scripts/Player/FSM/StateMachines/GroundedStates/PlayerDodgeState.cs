using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerGroundedState
{
    public PlayerDodgeState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.DodgeParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.DodgeParameterHash);
        GameManager.Instance.player.playerCollider.enabled = true;
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(GameManager.Instance.player.Animator, "Dodge");
        if(normalizedTime < 0.9f)
        {
            GameManager.Instance.player.playerCollider.enabled = false;
        }
        else
        {
            GameManager.Instance.player.Agent.ResetPath();
            GameManager.Instance.player.playerCollider.enabled = true;
            if (stateMachine.Player.Agent.velocity != Vector3.zero)
            {
                OnMove();
                return;
            }
            else
            {
                OnStand();
                return;
            }
        }
        
    }
}

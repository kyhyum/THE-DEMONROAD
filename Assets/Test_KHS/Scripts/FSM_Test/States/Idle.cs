using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private float _horizontalInput;

    public Idle(MovementSM stateMachine) : base("Idle", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Debug.Log("플레이어 FSM이 아이들 상태이다.");

        Debug.Log($"agent.remainingDistance: {playerInput.Agent.remainingDistance}");

        // remainingDistance: 목적지까지 남은 거리
        if (playerInput.Agent.remainingDistance > 10)
        {
            Debug.Log("에이전트가 이동하고 있다.");
        }

        //RaycastHit hit;
        //
        //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        //{
        //    Debug.Log("에이전트가 이동하고 있다.");
        //    stateMachine.ChangeState(((MovementSM)stateMachine).movingState);
        //}

        //

        //// agent가 이동하고 있는지 여부를 계산
        ////if (playerInput.Agent.velocity.sqrMagnitude >= 0.2f)
        //if (playerInput.Agent.velocity.magnitude >= 0.2f)
        //{
        //    Debug.Log("에이전트가 이동하고 있다.");
        //}
    }
}

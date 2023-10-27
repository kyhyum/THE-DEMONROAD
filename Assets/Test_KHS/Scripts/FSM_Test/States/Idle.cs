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

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Debug.Log("플레이어 FSM이 아이들 상태이다.");
            stateMachine.ChangeState(((MovementSM)stateMachine).movingState);
        }
    }
}

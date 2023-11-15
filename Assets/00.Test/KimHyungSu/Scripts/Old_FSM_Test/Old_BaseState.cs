using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Old_BaseState
{
    public string name;
    protected Old_StateMachine stateMachine;

    public PlayerInput playerInput;

    public Old_BaseState(string name, Old_StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;

    }

    public virtual void Enter()
    {

    }

    public virtual void UpdateLogic()
    {

    }

    public virtual void UpdatePhysics()
    {

    }

    public virtual void Exit()
    {

    }
}

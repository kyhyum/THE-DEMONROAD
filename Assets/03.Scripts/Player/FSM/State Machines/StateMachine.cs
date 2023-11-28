using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    // 현재 진행하고 있는 상태이다.
    protected IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();

        currentState = newState;
        
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void LateUpdate() 
    { 
        currentState?.LateUpdate();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }

    public void OnTriggerEnter(Collider other)
    {
        currentState?.OnTriggerEnter(other);
    }
}

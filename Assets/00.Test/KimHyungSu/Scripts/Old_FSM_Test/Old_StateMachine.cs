using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Old_StateMachine : MonoBehaviour
{
    Old_BaseState currentState;

    public Old_PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<Old_PlayerInput>();
    }

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    public void ChangeState(Old_BaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        //?
        //currentState.playerInput = playerInput;
        currentState.Enter();

    }

    protected virtual Old_BaseState GetInitialState()
    {
        return null;
    }

    //private void OnGUI()
    //{
    //    string content = currentState != null ? currentState.name : "(no current state)";
    //    GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    //}
}

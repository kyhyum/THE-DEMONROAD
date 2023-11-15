//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MovementSM : StateMachine
//{
//    [HideInInspector] public Idle idleState;
//    [HideInInspector] public Moving movingState;

//    private void Awake()
//    {
//        idleState = new Idle(this);
//        movingState = new Moving(this);

//        playerInput = GetComponent<PlayerInput>();
//    }

//    protected override BaseState GetInitialState()
//    {
//        return idleState;
//    }
//}

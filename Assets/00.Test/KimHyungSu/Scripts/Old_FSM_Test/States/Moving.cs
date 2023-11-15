//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class Moving : BaseState
//{
//    private MovementSM _sm;
//    private float _horizontalInput;
   
//    // TODO: 플레이어 NavMeshAgent 드래그앤드랍한다.
//    NavMeshAgent agent;

//    //agent.destination

//    public Moving(StateMachine stateMachine) : base("Moving", stateMachine)
//    {
//        _sm = (MovementSM)stateMachine;
//    }

//    public override void Enter()
//    {
//        base.Enter();
//    }

//    public override void UpdateLogic()
//    {
//        base.UpdateLogic();

//        Debug.Log("플레이어 FSM이 무빙 상태이다.");

//        // 목표지점에 도달하면 아이들 상태

//        Debug.Log($"agent.remainingDistance: {playerInput.Agent.remainingDistance}");

//        if (playerInput.Agent.remainingDistance < 5)
//        {
//            Debug.Log("에이전트가 목적지에 도착했다.");
//        }

//        //// playerInput.HitPoint.x, playerInput.HitPoint.z 과 playerInput.Agent.gameObject.transform.position.x, playerInput.Agent.gameObject.transform.position.z 값 비교한다.
//        //Vector2 playerVector = new Vector2(playerInput.Agent.gameObject.transform.position.x, playerInput.Agent.gameObject.transform.position.z);
//        //Vector2 hitVector = new Vector2(playerInput.HitPoint.x, playerInput.HitPoint.z);

//        //if (playerVector == hitVector)
//        //{
//        //    Debug.Log("에이전트가 목적지에 도착했다.");
//        //    stateMachine.ChangeState(((MovementSM)stateMachine).idleState);
//        //}

//        //

//        //RaycastHit hit;
//        //
//        //if (false == (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)))
//        //{
//        //    //Debug.Log("플레이어 FSM이 무빙 상태이다.");
//        //    stateMachine.ChangeState(((MovementSM)stateMachine).idleState);
//        //}

//        //

//        //// agent가 목적지에 도착했는지 여부를 계산
//        //if (playerInput.Agent.remainingDistance <= 0.5f)
//        //{
//        //    Debug.Log("에이전트가 목적지에 도착했다.");
//        //}
//    }

//    public override void UpdatePhysics()
//    {
//        base.UpdatePhysics();
//    }
//}

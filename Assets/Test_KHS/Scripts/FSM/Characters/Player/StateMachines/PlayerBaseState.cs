using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        Move();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;

        //input.PlayerActions.Movement.canceled += OnMovementCanceled;
        //input.PlayerActions.Run.started += OnRunStarted;

        //input.PlayerActions.Move.Enable();
        input.PlayerActions.Move.started += OnInputMove;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;

        //input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        //input.PlayerActions.Run.started -= OnRunStarted;

        //input.PlayerActions.Move.Disable();
        input.PlayerActions.Move.started -= OnInputMove;
    }

    private NavMeshAgent agent;

    private Vector3 hitPoint;
    public Vector3 HitPoint => hitPoint;

    private void OnInputMove(InputAction.CallbackContext callbackContext)
    {
       //Debug.Log("PlayerInput 클래스 OnInputMove 함수 출력");

        

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            hitPoint = hit.point;
            //Debug.Log($"hitPoint:{hitPoint}");

            stateMachine.Player.Agent.SetDestination(hit.point);
            //agent.destination = hitPoint;
        }
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    private void ReadMovementInput()
    {
        //stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        //Vector3 movementDirection = GetMovementDirection();
        //
        //Rotate(movementDirection);
        //
        //Move(movementDirection);
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private float GetMovemenetSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }

    }
}

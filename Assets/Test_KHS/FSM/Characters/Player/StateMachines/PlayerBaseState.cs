using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Windows;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = playerStateMachine.Player.Data.GroundedData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();

        
    }

    /// <summary>
    /// Movement에 대한 Input을 가져온다.
    /// </summary>
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

    public virtual void LateUpdate()
    {
        LateMove();
    }

    /// <summary>
    /// Add
    /// </summary>
    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled += OnMoveCanceled;
    }

    /// <summary>
    /// Remove
    /// </summary>
    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled -= OnMoveCanceled;
    }

    // Move.canceled: 무브가 떼어졌을 때, 마우스 오른쪽 버튼이 떨어졌을 때 발생하는 이벤트이다.
    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, 100))
        {
            Debug.Log($"hit.collider.name: {hit.collider.name}");
            Debug.Log($"hit.point: {hit.point}");

            stateMachine.Player.Agent.SetDestination(hit.point);
        }
    }

    /// <summary>
    /// 이동 입력값을 읽어온다.
    /// </summary>
    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Move.ReadValue<float>();
    }

    /// <summary>
    /// 실제 이동하는 처리를 한다.
    /// </summary>
    private void Move()
    {
        float movementSpeed = GetMovemenetSpeed();

        if (stateMachine.Player.Agent.remainingDistance > stateMachine.Player.Agent.stoppingDistance)
        {
            stateMachine.Player.Controller.Move((stateMachine.Player.Agent.velocity.normalized * movementSpeed) * Time.deltaTime);
        }
        else
        {
            stateMachine.Player.Controller.Move(Vector3.zero);
        }
    }

    private void LateMove()
    {
        stateMachine.Player.transform.position = stateMachine.Player.Agent.nextPosition;
    }

    private float GetMovemenetSpeed()
    {
        // 실제로 이동해야 되는 속도가 나온다.
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    #region State용 애니메이션 처리 함수
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }
    
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }
    #endregion State용 애니메이션 처리 함수
}

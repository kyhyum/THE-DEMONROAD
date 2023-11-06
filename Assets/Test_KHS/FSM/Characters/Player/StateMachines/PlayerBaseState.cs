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
        // .started: 해당 키가 눌려졌을 때
        input.PlayerActions.Move.started += OnMoveStarted;
    }

    /// <summary>
    /// Remove
    /// </summary>
    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.started -= OnMoveStarted;
    }

    protected virtual void OnMoveStarted(InputAction.CallbackContext context)
    {
        Debug.Log("OnMoveStarted 함수 호출한다.");

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, 100))
        {
            Debug.Log($"hit.collider.name: {hit.collider.name}");
            Debug.Log($"hit.point: {hit.point}");

            stateMachine.Player.Agent.SetDestination(hit.point);
        }
    }

    /// <summary>
    /// 실제 이동하는 처리를 한다.
    /// </summary>
    private void Move()
    {

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

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
        // .performed: 해당 키가 눌려지고 있는 동안에
        input.PlayerActions.Attack.performed += OnAttackPerformed;
        // .canceled: (눌려져 있는) 해당 키가 떼어졌을 떄
        input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }

    /// <summary>
    /// Remove
    /// </summary>
    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.started -= OnMoveStarted;
        input.PlayerActions.Attack.performed -= OnAttackPerformed;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }

    protected virtual void OnMoveStarted(InputAction.CallbackContext context)
    {
        //Debug.Log("OnMoveStarted 함수 호출한다.");

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, 100))
        {
            //Debug.Log($"hit.collider.name: {hit.collider.name}");
            //Debug.Log($"hit.point: {hit.point}");

            stateMachine.Player.Agent.SetDestination(hit.point);
        }
    }

    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("OnAttackPerformed 함수 호출한다.");

        stateMachine.Player.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log("OnAttackCanceled 함수 호출한다.");

        stateMachine.Player.IsAttacking = false;
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

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        // Animator.GetCurrentAnimatorStateInfo(): 현재 애니메이터 상태 정보 구한다. 애니메이터의 현재 상태에서 데이터를 가져온다. 이를 사용하여 상태의 속도, 길이, 이름 및 기타 변수에 액세스하는 것을 포함하여 상태의 세부 정보를 가져온다.
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Animator.GetNextAnimatorStateInfo(): 다음 애니메이터 스테이트 정보 구한다.
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        // Animator.IsInTransition(): 지정된 레이어에 전환이 있으면 true를 반환하고, 그렇지 않으면 false를 반환한다.
        // AnimatorStateInfo.IsTag(string tag): 상태 머신의 활성 상태 태그와 일치한다.
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            // AnimatorStateInfo.normalizedTime(): 상태의 정규화된 시간이다. 정수 부분은 상태가 반복된 횟수이다.소수 부분은 현재 루프에서 진행률의 % (0 - 1)이다.
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

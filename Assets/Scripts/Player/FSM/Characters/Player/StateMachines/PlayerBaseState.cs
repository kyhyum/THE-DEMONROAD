using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    
    float cameraDistance;
    [field: SerializeField] public float minCamDistance = 5.0f;
    [field: SerializeField] public float maxCamDistance = 15.0f;
    [field: SerializeField] float sensitivity = 1f;

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
        // 
        input.PlayerActions.MouseScrollY.performed += OnMouseScrollYPerformed;
        //
        input.PlayerActions.QuickSlot1.performed += OnQuickSlot1Performed;
        input.PlayerActions.QuickSlot1.canceled += OnQuickSlot1Canceled;
        input.PlayerActions.QuickSlot2.performed += OnQuickSlot2Performed;
        input.PlayerActions.QuickSlot2.canceled += OnQuickSlot2Canceled;
        input.PlayerActions.QuickSlot3.performed += OnQuickSlot3Performed;
        input.PlayerActions.QuickSlot3.canceled += OnQuickSlot3Canceled;
        input.PlayerActions.QuickSlot4.performed += OnQuickSlot4Performed;
        input.PlayerActions.QuickSlot4.canceled += OnQuickSlot4Canceled;
        input.PlayerActions.QuickSlot5.performed += OnQuickSlot5Performed;
        input.PlayerActions.QuickSlot5.canceled += OnQuickSlot5Canceled;
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
        input.PlayerActions.MouseScrollY.performed -= OnMouseScrollYPerformed;

        input.PlayerActions.QuickSlot1.performed -= OnQuickSlot1Performed;
        input.PlayerActions.QuickSlot1.canceled -= OnQuickSlot1Canceled;
        input.PlayerActions.QuickSlot2.performed -= OnQuickSlot2Performed;
        input.PlayerActions.QuickSlot2.canceled -= OnQuickSlot2Canceled;
        input.PlayerActions.QuickSlot3.performed -= OnQuickSlot3Performed;
        input.PlayerActions.QuickSlot3.canceled -= OnQuickSlot3Canceled;
        input.PlayerActions.QuickSlot4.performed -= OnQuickSlot4Performed;
        input.PlayerActions.QuickSlot4.canceled -= OnQuickSlot4Canceled;
        input.PlayerActions.QuickSlot5.performed -= OnQuickSlot5Performed;
        input.PlayerActions.QuickSlot5.canceled -= OnQuickSlot5Canceled;
    }

    protected virtual void OnMoveStarted(InputAction.CallbackContext context)
    {
        //Debug.Log("OnMoveStarted 함수 호출한다.");

        Move();
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

    protected virtual void OnMouseScrollYPerformed(InputAction.CallbackContext context)
    {
        CinemachineComponentBase componentBase = stateMachine.Player.ComponentBase;

        //Debug.Log("OnMouseScrollYPerformed 함수 호출한다.");

        cameraDistance = context.ReadValue<float>() * sensitivity;
        
        //Debug.Log($"cameraDistance : {cameraDistance}");

        if (componentBase is CinemachineFramingTransposer)
        {
            CinemachineFramingTransposer framingTransposer = componentBase as CinemachineFramingTransposer;

            framingTransposer.m_CameraDistance -= cameraDistance;

            framingTransposer.m_CameraDistance = Mathf.Clamp(framingTransposer.m_CameraDistance, minCamDistance, maxCamDistance);
        }

    }

    protected virtual void OnQuickSlot1Performed(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot1Performed 함수 호출한다.");
    }

    protected virtual void OnQuickSlot1Canceled(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot1Canceled 함수 호출한다.");
    }

    protected virtual void OnQuickSlot2Performed(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot2Performed 함수 호출한다.");
    }

    protected virtual void OnQuickSlot2Canceled(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot2Canceled 함수 호출한다.");
    }

    protected virtual void OnQuickSlot3Performed(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot3Performed 함수 호출한다.");
    }

    protected virtual void OnQuickSlot3Canceled(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot3Canceled 함수 호출한다.");
    }

    protected virtual void OnQuickSlot4Performed(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot4Performed 함수 호출한다.");
    }

    protected virtual void OnQuickSlot4Canceled(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot4Canceled 함수 호출한다.");
    }

    protected virtual void OnQuickSlot5Performed(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot5Performed 함수 호출한다.");
    }

    protected virtual void OnQuickSlot5Canceled(InputAction.CallbackContext context)
    {
        Debug.Log("OnQuickSlot5Canceled 함수 호출한다.");
    }

    /// <summary>
    /// 실제 이동하는 처리를 한다.
    /// </summary>
    protected void Move()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, 100))
        {
            //Debug.Log($"hit.collider.name: {hit.collider.name}");
            //Debug.Log($"hit.point: {hit.point}");

            stateMachine.Player.Agent.SetDestination(hit.point);
        }
    }

    private void LateMove()
    {
        stateMachine.Player.transform.position = stateMachine.Player.Agent.nextPosition;
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

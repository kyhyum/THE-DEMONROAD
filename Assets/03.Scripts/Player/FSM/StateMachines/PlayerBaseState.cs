using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;


    RaycastHit hit;

    Vector3 destPosition;
    Vector3 direction;
    Quaternion lookTarget;

    //Camera
    float cameraDistance;
    [field: SerializeField] public float minCamDistance = 5.0f;
    [field: SerializeField] public float maxCamDistance = 25.0f;
    [field: SerializeField] float sensitivity = 1f;
    Vector2 currentCursorPosition;


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
    public virtual void Update()
    {
        PerformedMove(); 
    }

    public virtual void PhysicsUpdate()
    {
        
    }


    public virtual void LateUpdate()
    {
        LateMove();
    }

    public virtual void OnTriggerEnter(Collider other)
    {

    }

    /// <summary>
    /// Add
    /// </summary>
    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = GameManager.Instance.player.Input;
        // .started: 해당 키가 눌려졌을 때
        
        input.PlayerActions.Move.started += OnMoveStarted;
        input.PlayerActions.Move.performed += OnMovePerformed;
        input.PlayerActions.Move.canceled += OnMoveCanceled;
        // .performed: 해당 키가 눌려지고 있는 동안에
        input.PlayerActions.Attack.performed += OnAttackPerformed;
        // .canceled: (눌려져 있는) 해당 키가 떼어졌을 떄
        input.PlayerActions.Attack.canceled += OnAttackCanceled;

        input.PlayerActions.MouseScrollY.performed += OnMouseScrollYPerformed;
        input.PlayerActions.MouseScrollClick.performed += OnMouseScrollClickPerformed;
        input.PlayerActions.Dodge.started += OnDodgeStarted;
        input.PlayerActions.Dodge.canceled += OnDodgeCanceled;
    } 

    /// <summary>
    /// Remove
    /// </summary>
    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = GameManager.Instance.player.Input;

        input.PlayerActions.Move.started -= OnMoveStarted;
        input.PlayerActions.Move.performed -= OnMovePerformed;
        input.PlayerActions.Move.canceled -= OnMoveCanceled;

        input.PlayerActions.Attack.performed -= OnAttackPerformed;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
        input.PlayerActions.MouseScrollY.performed -= OnMouseScrollYPerformed;
        input.PlayerActions.MouseScrollClick.performed -= OnMouseScrollClickPerformed;
        input.PlayerActions.Dodge.started -= OnDodgeStarted;
        input.PlayerActions.Dodge.canceled -= OnDodgeCanceled;
    }


    #region Move
    protected virtual void OnMoveStarted(InputAction.CallbackContext context)
    {
        //Debug.Log("OnMoveStarted 함수 호출한다.");

        Move();
    }

    protected virtual void OnMovePerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("OnMovePerformed 함수 호출한다.");

        GameManager.Instance.player.IsMovePerformed = true;
    }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log("OnMoveCanceled 함수 호출한다.");

        GameManager.Instance.player.IsMovePerformed = false;
    }

    #endregion Move

    #region Attack
    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("OnAttackPerformed 함수 호출한다.");

        Rotate();

        GameManager.Instance.player.IsAttacking = true;
        GameManager.Instance.player.IsMovePerformed = false;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log("OnAttackCanceled 함수 호출한다.");

        GameManager.Instance.player.IsAttacking = false;
    }

    #endregion Attack


    #region ScrollClick
    protected virtual void OnMouseScrollClickPerformed(InputAction.CallbackContext context)
    {
        if (!Mouse.current.middleButton.isPressed)
            return;

        float inputValue = context.ReadValue<Vector2>().x;
        Debug.Log(inputValue);
        GameManager.Instance.player.VirtualCamera.transform.rotation = Quaternion.Euler(45f, inputValue + GameManager.Instance.player.VirtualCamera.transform.rotation.eulerAngles.y, 0f);
    }

    protected virtual void OnMouseScrollYPerformed(InputAction.CallbackContext context)
    {
        CinemachineComponentBase componentBase = GameManager.Instance.player.ComponentBase;

        //Debug.Log("OnMouseScrollYPerformed 함수 호출한다.");

        cameraDistance = context.ReadValue<float>() * sensitivity;
        
        //Debug.Log($"cameraDistance : {cameraDistance}");

        if (componentBase is CinemachineFramingTransposer)
        {
            CinemachineFramingTransposer framingTransposer = componentBase as CinemachineFramingTransposer;

            float currentCameraDistance = framingTransposer.m_CameraDistance;
            float changeCameraDistance = currentCameraDistance;
            if (cameraDistance > 0)
            {
                changeCameraDistance = (changeCameraDistance - 5 >= minCamDistance) ? changeCameraDistance - 5 : minCamDistance;

                framingTransposer.m_CameraDistance = Mathf.Clamp(currentCameraDistance, currentCameraDistance, changeCameraDistance);
            }
            else if(cameraDistance < 0)
            {
                changeCameraDistance = (changeCameraDistance + 5 <= maxCamDistance) ? changeCameraDistance + 5 : maxCamDistance;

                framingTransposer.m_CameraDistance = Mathf.Clamp(currentCameraDistance, changeCameraDistance, currentCameraDistance);
            }
        }
    }

    #endregion ScrollClick

    #region Dodge
    protected virtual void OnDodgeStarted(InputAction.CallbackContext context)
    {
        Move();
        GameManager.Instance.player.IsDodging = true;
    }
    protected virtual void OnDodgeCanceled(InputAction.CallbackContext context)
    {
        GameManager.Instance.player.IsDodging = false;
    }

    #endregion Dodge

    /// <summary>
    /// 실제 이동하는 처리를 한다.
    /// </summary>
    protected void Move()
    {
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).direction.normalized, 100, 1 << LayerMask.NameToLayer("Ground"));
        if (hits.Length > 0)
        {
            //Debug.Log($"hit.collider.name: {hit.collider.name}");
            //Debug.Log($"hit.point: {hit.point}");
            destPosition = new Vector3(hits[0].point.x, GameManager.Instance.player.transform.position.y, hits[0].point.z); 
            direction = destPosition - GameManager.Instance.player.transform.position;
            lookTarget = Quaternion.LookRotation(direction);
            GameManager.Instance.player.transform.rotation = lookTarget;
            GameManager.Instance.player.Agent.SetDestination(hits[0].point);
        }
    }

    protected void PerformedMove()
    {
        if (!GameManager.Instance.player.IsMovePerformed)
            return;

        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).direction.normalized, 100, 1 << LayerMask.NameToLayer("Ground"));
        if (hits.Length > 0)
        {
            //Debug.Log($"hit.collider.name: {hit.collider.name}");
            //Debug.Log($"hit.point: {hit.point}");
            destPosition = new Vector3(hits[0].point.x, GameManager.Instance.player.transform.position.y, hits[0].point.z);
            direction = destPosition - GameManager.Instance.player.transform.position;
            lookTarget = Quaternion.LookRotation(direction);
            GameManager.Instance.player.transform.rotation = lookTarget;
            GameManager.Instance.player.Agent.SetDestination(hits[0].point);
        }
    }



    private void LateMove()
    {
        GameManager.Instance.player.transform.position = GameManager.Instance.player.Agent.nextPosition;
    }

    private void Rotate()
    {
        Player player = GameManager.Instance.player;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            destPosition = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);

            direction = destPosition - player.transform.position;

            lookTarget = Quaternion.LookRotation(direction);
            player.transform.rotation = lookTarget;
        }
    }

    #region State용 애니메이션 처리 함수
    protected void StartAnimation(int animationHash)
    {
        GameManager.Instance.player.Animator.SetBool(animationHash, true);
    }
    
    protected void StopAnimation(int animationHash)
    {
        GameManager.Instance.player.Animator.SetBool(animationHash, false);
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

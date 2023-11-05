using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        // TODO
        //groundData = playerStateMachine.Data.GroundData;
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

    /// <summary>
    /// Add
    /// </summary>
    protected virtual void AddInputActionsCallbacks()
    {

    }

    /// <summary>
    /// Remove
    /// </summary>
    protected virtual void RemoveInputActionsCallbacks()
    {

    }

    /// <summary>
    /// 이동 입력값을 읽어온다.
    /// </summary>
    private void ReadMovementInput()
    {
        // stateMachine에 역참조를 계속 간다. 참조만 따라가면 된다.
        // 이제부터 자주 사용되는 애들은 캐싱을 해놓는게 아무래도 좋다.
        // TODO
        //stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    /// <summary>
    /// 실제 이동하는 처리를 한다.
    /// </summary>
    private void Move()
    {
        // 이동 방향이다.
        Vector3 movementDirection = GetMovementDirection();
        
        Rotate(movementDirection);

        Move(movementDirection);
    }

    /// <summary>
    /// 이동해야하는 방향을 가져온다. 카메라가 바라보고 있는 방향으로 이동을 하게 만들 것이다.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMovementDirection()
    {
        // 메인 카메라가 바라보고 있는 정면이다.
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        // 메인 카메라 right 방향이다.
        Vector3 right = stateMachine.MainCameraTransform.right;

        // y값을 제거해야지 땅바닥 보고 안간다.
        forward.y = 0;
        right.y = 0;

        // Normalize(): 저 벡터 자체를 normalize한다.
        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    /// <summary>
    /// 이동
    /// </summary>
    /// <param name="movementDirection"></param>
    private void Move(Vector3 movementDirection)
    {
        // 이 스피드를 가지고서 실제로 이동 처리를 한다.
        float movementSpeed = GetMovemenetSpeed();
        // 컨트롤러를 쓸 것이기 때문에 Controller.Move를 쓴다.
        stateMachine.Player.Controller.Move(
            // 이동 처리를 한다.
            // 이동 방향과 스피드를 곱해 주는 것이다.
            // 그런 다음에 곱하기해서 deltaTime 해주면 끝이다.

            // 나중에 여기서 기능이 조금 더 추가될 것이다.
            (movementDirection * movementSpeed) * Time.deltaTime
            );
    }

    /// <summary>
    /// 회전
    /// </summary>
    /// <param name="movementDirection"></param>
    private void Rotate(Vector3 movementDirection)
    {
        // 뭔가 입력이 됐을 때 
        if (movementDirection != Vector3.zero)
        {
            // stateMachine.Player.transform이 너무 자주 나와서 줄였다.
            Transform playerTransform = stateMachine.Player.transform;
            // 그 방향을 바라보는 로테이션을 만들어 줄 것이다.
            // Quaternion.LookRotation(Vector3 forward) : forward 방향을 바라보는 쿼터니언을 만들어준다.
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            // stateMachine.Player.transform.rotation 값을 바꿔주면 되는데, 이러면 너무 순간이동하듯이 뺑뺑 도니까
            // 러프인데 S러프 그러니까 선형 보간이 아니라 타원의 곡선의 보간이 일어난다.
            // Quaternion.Slerp(): 타원의 곡선의 보간을 해준다.
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
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

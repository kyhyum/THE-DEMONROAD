using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseState : MonsterIState
{
    protected MonsterStateMachine stateMachine;
    public MonsterBaseState(MonsterStateMachine monsterStateMachine) {
        stateMachine = monsterStateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
        Rotate();
    }

    //private void Move()
    //{
    //    Vector3 movementDirection = GetMovementDirection();
    //    Rotate(movementDirection);
    //    Move(movementDirection);
    //}

    private void Rotate()
    {
        Vector3 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
    }

    protected void ForceMove()
    {
        stateMachine.Monster.Controller.Move(stateMachine.Monster.EnemyForceReceiver.Movement * Time.deltaTime);
    }

    // 
    private Vector3 GetMovementDirection()
    {
        return (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).normalized;
    }

    //private void Move(Vector3 direction)
    //{
    //    float movementSpeed = GetMovementSpeed();
    //    stateMachine.Monster.Controller.Move(((direction * movementSpeed) + stateMachine.Monster.EnemyForceReceiver.Movement) * Time.deltaTime);
    //}

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            stateMachine.Monster.transform.rotation = Quaternion.Slerp(stateMachine.Monster.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;

        return movementSpeed;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, false);
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

    protected bool IsInChaseRange()
    {
        // if (stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Monster.Data.PlayerChasingRange * stateMachine.Monster.Data.PlayerChasingRange;
    }
}

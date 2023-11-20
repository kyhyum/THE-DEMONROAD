using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : BossIState
{
    protected BossStateMachine stateMachine;
    public BossBaseState(BossStateMachine bossStateMachine) {
        stateMachine = bossStateMachine;
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


    private Vector3 GetMovementDirection()
    {
        return (stateMachine.Target.transform.position - stateMachine.Boss.transform.position).normalized;
    }


    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            stateMachine.Boss.transform.rotation = Quaternion.Slerp(stateMachine.Boss.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }


    protected void StartAnimation(int animationHash)
    {
        stateMachine.Boss.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Boss.Animator.SetBool(animationHash, false);
    }

    protected float GetNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);

        return currentInfo.normalizedTime;
    }

    protected bool IsInChaseRange()
    {
        // if (stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Boss.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Boss.Data.PlayerChasingRange * stateMachine.Boss.Data.PlayerChasingRange;
    }
}

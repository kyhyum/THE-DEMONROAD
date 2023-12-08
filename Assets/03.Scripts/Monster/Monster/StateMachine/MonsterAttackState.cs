using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    private bool alreadyAppliedDealing;
    public MonsterAttackState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        stateMachine = monsterStateMachine;
    }
    public override void Enter()
    {
        alreadyAppliedDealing = false;
        stateMachine.MovementSpeedModifier = 0;
        stateMachine.Monster.monsterSound.PlayAttackSound();
        base.Enter();
        if(GetNormalizedTime(stateMachine.Monster.Animator, "Attack") > 1)
        {
            stateMachine.Monster.Animator.Play("Attack", -1, 0f);
        }
        else
        {
            StartAnimation(stateMachine.Monster.monsterAnimationData.AttackParameterHash);
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.monsterAnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Monster.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Monster.Data.Dealing_Start_TransitionTime && normalizedTime < stateMachine.Monster.Data.Dealing_End_TransitionTime)
            {
                stateMachine.Monster.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }else if (alreadyAppliedDealing && normalizedTime >= stateMachine.Monster.Data.Dealing_End_TransitionTime)
            {
                alreadyAppliedDealing = false;
                stateMachine.Monster.Weapon.gameObject.SetActive(false);
            }

        }
        else
        {
            alreadyAppliedDealing = false;
            if (IsInAttackRange())
            {
                stateMachine.ChangeState(stateMachine.AttackState);
                return;
            }
            else if (IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
        }
    }
    private bool IsInAttackRange()
    {
        // if (stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Monster.Data.AttackRange * stateMachine.Monster.Data.AttackRange;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    private bool alreadyAppliedDealing;
    private bool alreadyAppliedForce;
    public MonsterAttackState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        stateMachine = monsterStateMachine;
    }
    public override void Enter()
    {
        alreadyAppliedForce = false;
        alreadyAppliedDealing = false;
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Monster.monsterAnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.monsterAnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();
        float normalizedTime = GetNormalizedTime(stateMachine.Monster.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (!stateMachine.Monster.Data.IsLongRanged)
            {
                if (normalizedTime >= stateMachine.Monster.Data.ForceTransitionTime)
                    TryApplyForce();
            }

            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Monster.Data.Dealing_Start_TransitionTime)
            {
                stateMachine.Monster.Weapon.SetAttack(stateMachine.Monster.Data.Damage);
                stateMachine.Monster.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }

            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Monster.Data.Dealing_End_TransitionTime)
            {
                stateMachine.Monster.Weapon.gameObject.SetActive(false);
                alreadyAppliedDealing = false;
            }

        }
        else
        {
            if (IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
        }
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Monster.EnemyForceReceiver.Reset();

        stateMachine.Monster.EnemyForceReceiver.AddForce(stateMachine.Monster.transform.forward * stateMachine.Monster.Data.Force);

    }
}

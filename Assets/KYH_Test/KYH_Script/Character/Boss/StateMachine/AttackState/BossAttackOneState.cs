using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일반 근접 공격
public class BossAttackOneState : BossBaseState
{
    private bool alreadyAppliedDealing;
    private bool alreadyAppliedForce;
    public BossAttackOneState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }
    public override void Enter()
    {
        alreadyAppliedForce = false;
        alreadyAppliedDealing = false;
        base.Enter();
        StartAnimation(stateMachine.Boss.bossAnimationData.Attack1ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Boss.bossAnimationData.Attack1ParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();
        float normalizedTime = GetNormalizedTime(stateMachine.Boss.Animator, "Attack1");
        if (normalizedTime < 1f)
        {
            if (!stateMachine.Boss.Data.AttackPatternInfoDatas[0].IsLongRanged)
            {
                if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[0].ForceTransitionTime)
                    TryApplyForce();
            }

            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[0].Dealing_Start_TransitionTime)
            {
                stateMachine.Boss.Weapon.SetAttack(stateMachine.Boss.Data.AttackPatternInfoDatas[0].Damage);
                stateMachine.Boss.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }else if (alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[0].Dealing_End_TransitionTime)
            {
                stateMachine.Boss.Weapon.gameObject.SetActive(false);
                alreadyAppliedDealing = false; 
                if (IsInChaseRange())
                {
                    stateMachine.ChangeState(stateMachine.ChasingState);
                    return;
                }
            }

        }
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Boss.EnemyForceReceiver.Reset();

        stateMachine.Boss.EnemyForceReceiver.AddForce(stateMachine.Boss.transform.forward * stateMachine.Boss.Data.AttackPatternInfoDatas[0].Force);

    }

}

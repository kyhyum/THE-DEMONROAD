using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//패턴 원거리 강공격
public class BossAttackFourState : BossBaseState
{
    private bool alreadyAppliedDealing;
    private bool alreadyAppliedForce;
    public BossAttackFourState(BossStateMachine bossStateMachine) : base(bossStateMachine)
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
        float normalizedTime = GetNormalizedTime(stateMachine.Boss.Animator, "Attack4");
        if (normalizedTime < 1f)
        {
            if (!stateMachine.Boss.Data.AttackPatternInfoDatas[3].IsLongRanged)
            {
                if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[3].ForceTransitionTime)
                    TryApplyForce();
            }

            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[3].Dealing_Start_TransitionTime)
            {
                stateMachine.Boss.Weapon.SetAttack(stateMachine.Boss.Data.AttackPatternInfoDatas[3].Damage);
                alreadyAppliedDealing = true;
            }else if (alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[3].Dealing_End_TransitionTime)
            {
                alreadyAppliedDealing = false; 
            }

        }
        else
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Boss.EnemyForceReceiver.Reset();

        stateMachine.Boss.EnemyForceReceiver.AddForce(stateMachine.Boss.transform.forward * stateMachine.Boss.Data.AttackPatternInfoDatas[3].Force);

    }

}

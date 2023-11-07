using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일반 원거리 공격
public class BossAttackThreeState : BossBaseState
{
    private bool alreadyAppliedDealing;
    private bool alreadyAppliedForce;
    public BossAttackThreeState(BossStateMachine bossStateMachine) : base(bossStateMachine)
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
        float normalizedTime = GetNormalizedTime(stateMachine.Boss.Animator, "Attack3");
        if (normalizedTime < 1f)
        {
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[2].Dealing_Start_TransitionTime)
            {
                stateMachine.Boss.Weapon.SetAttack(stateMachine.Boss.Data.AttackPatternInfoDatas[2].Damage);
                alreadyAppliedDealing = true;
            }else if (alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[2].Dealing_End_TransitionTime)
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

}

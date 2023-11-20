using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//패턴 강공격
public class BossAttackTwoState : BossBaseState
{
    private bool alreadyAppliedDealing;
    private bool alreadyAppliedForce;
    public BossAttackTwoState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }
    public override void Enter()
    {
        alreadyAppliedForce = false;
        alreadyAppliedDealing = false;
        base.Enter();
        StartAnimation(stateMachine.Boss.bossAnimationData.Attack2ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Boss.bossAnimationData.Attack2ParameterHash);
    }

    public override void Update()
    {
        //강공은 회전있으면 너무 빡셀거 같아 base.update는 없엠
        float normalizedTime = GetNormalizedTime(stateMachine.Boss.Animator);
        if (normalizedTime < 1f)
        {
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[1].Dealing_Start_TransitionTime)
            {
                stateMachine.Boss.Weapon.SetAttack(stateMachine.Boss.Data.AttackPatternInfoDatas[1].Damage);
                stateMachine.Boss.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }else if (alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[1].Dealing_End_TransitionTime)
            {
                stateMachine.Boss.Weapon.gameObject.SetActive(false);
                alreadyAppliedDealing = false; 
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }

        }
    }
}

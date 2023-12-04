using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//패턴 강공격
public class BossAttackTwoState : BossBaseState
{
    private bool alreadyAppliedDealing;
    bool isAttack = false;
    public BossAttackTwoState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }
    public override void Enter()
    {
        alreadyAppliedDealing = false;
        isAttack = false;
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
        float normalizedTime = GetNormalizedTime(stateMachine.Boss.Animator);
        if (normalizedTime < 1f)
        {
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[1].Dealing_Start_TransitionTime && normalizedTime < stateMachine.Boss.Data.AttackPatternInfoDatas[1].Dealing_End_TransitionTime)
            {
                stateMachine.Boss.Weapon2.SetAttack(stateMachine.Boss.Data.AttackPatternInfoDatas[1].Damage);
                if (!stateMachine.Boss.Weapon2.gameObject.activeSelf)
                {
                    stateMachine.Boss.Weapon2.gameObject.SetActive(true);
                }
                alreadyAppliedDealing = true;
            }else if (alreadyAppliedDealing && normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[1].Dealing_End_TransitionTime && !isAttack)
            {
                if (stateMachine.Boss.Weapon2.gameObject.activeSelf)
                {
                    stateMachine.Boss.Weapon2.gameObject.SetActive(false);
                }
                alreadyAppliedDealing = false;
                isAttack = true;
                return;
            }

        }
        else if(normalizedTime > 1f && isAttack)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
    }
}

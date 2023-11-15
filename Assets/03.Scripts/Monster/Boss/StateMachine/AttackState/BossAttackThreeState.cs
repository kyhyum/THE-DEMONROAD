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
        StartAnimation(stateMachine.Boss.bossAnimationData.Attack2ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Boss.bossAnimationData.Attack2ParameterHash);
    }

    public override void Update()
    {

        float normalizedTime = GetNormalizedTime(stateMachine.Boss.Animator, "Attack3");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[2].Dealing_Start_TransitionTime)
            {
                BossBullet bossBullet = stateMachine.Boss.pattern1Bullet.GetObject();
                bossBullet.BulletReturned += stateMachine.Boss.pattern1Bullet.ReturnObject;
                bossBullet.Shooting();
            }
            else if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[2].Dealing_End_TransitionTime)
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }

        }
    }

}

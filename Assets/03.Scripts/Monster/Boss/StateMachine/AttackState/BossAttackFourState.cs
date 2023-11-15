using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//패턴 원거리 강공격
public class BossAttackFourState : BossBaseState
{
    public BossAttackFourState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Boss.bossAnimationData.Attack3ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Boss.bossAnimationData.Attack3ParameterHash);
    }

    public override void Update()
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Boss.Animator, "Attack4");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[3].Dealing_Start_TransitionTime)
            {
                BossBullet bossBullet = stateMachine.Boss.pattern2Bullet.GetObject();
                bossBullet.BulletReturned += stateMachine.Boss.pattern2Bullet.ReturnObject;
            }
            else if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[3].Dealing_End_TransitionTime)
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }

        }
    }

}

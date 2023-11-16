using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//패턴 원거리 강공격
public class BossAttackFourState : BossBaseState
{
    private float timer = 0f;
    public float interval = 2f;
    public BossAttackFourState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Boss.bossAnimationData.Attack4ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Boss.bossAnimationData.Attack4ParameterHash);
    }

    public override void Update()
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Boss.Animator);
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[3].Dealing_Start_TransitionTime)
            {
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    BossBullet bossBullet = stateMachine.Boss.pattern2Bullet.GetObject();
                    bossBullet.gameObject.transform.position = stateMachine.Target.position;
                    bossBullet.BulletReturned += stateMachine.Boss.pattern2Bullet.ReturnObject;
                    timer = 0f; // 타이머 초기화
                }
            }
        }
        else
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }

}

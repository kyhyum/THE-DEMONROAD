using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일반 원거리 공격
public class BossAttackThreeState : BossBaseState
{

    private float timer = 0f;
    public float interval = 1.2f;

    public BossAttackThreeState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        timer = 0f;
        StartAnimation(stateMachine.Boss.bossAnimationData.Attack3ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Boss.bossAnimationData.Attack3ParameterHash);
    }

    public override void Update()
    {
        base.Update();
        float normalizedTime = GetNormalizedTime(stateMachine.Boss.Animator);
        if (normalizedTime < 1f)
        {
            Debug.Log(normalizedTime);
            if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[2].Dealing_Start_TransitionTime)
            {
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    BossBullet bossBullet = stateMachine.Boss.pattern1Bullet.GetObject();
                    bossBullet.BulletSetEventNull();
                    bossBullet.gameObject.transform.position = stateMachine.Boss.bulletSpawnPoint.position;
                    bossBullet.BulletReturned += stateMachine.Boss.pattern1Bullet.ReturnObject;
                    bossBullet.Shooting();
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

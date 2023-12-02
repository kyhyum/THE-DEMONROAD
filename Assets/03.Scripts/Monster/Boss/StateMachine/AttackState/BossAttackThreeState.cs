using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일반 원거리 공격
public class BossAttackThreeState : BossBaseState
{

    private float timer = 0f;
    public float interval = 1.2f;
    private bool isCast = false;

    public BossAttackThreeState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Boss.bossAnimationData.Attack3ParameterHash);
        timer = 0f;
        isCast = false;
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
            if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[2].Dealing_Start_TransitionTime && !isCast)
            {
                stateMachine.Boss.bossBullet1CastSpell.SetActive(true);
                isCast = true;
            }
            else if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[2].Dealing_End_TransitionTime && normalizedTime <= 0.9f)
            {
                timer += Time.deltaTime;
                if(timer > 0.5f)
                {
                    BossBullet bossBullet = stateMachine.Boss.pattern1Bullet.GetObject();
                    bossBullet.BulletSetEventNull();
                    bossBullet.gameObject.transform.position = stateMachine.Boss.bulletSpawnPoint.position;
                    bossBullet.BulletReturned += stateMachine.Boss.pattern1Bullet.ReturnObject;
                    bossBullet.Shooting();
                    timer = 0;
                }
            }

        }
        else
        {
            stateMachine.Boss.bossBullet1CastSpell.SetActive(false);
            isCast = false;
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }

}

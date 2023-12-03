using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//패턴 원거리 강공격
public class BossAttackFourState : BossBaseState
{
    public float interval = 2f;
    bool isAttack = false;
    public BossAttackFourState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        isAttack = false;
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
            
            if (normalizedTime >= stateMachine.Boss.Data.AttackPatternInfoDatas[3].Dealing_Start_TransitionTime && !isAttack)
            {
                BossSpell bossSpell = stateMachine.Boss.pattern2Bullet.GetObject();
                bossSpell.SpellSetEventNull();
                bossSpell.gameObject.transform.position = stateMachine.Boss.transform.position;
                bossSpell.BulletReturned += stateMachine.Boss.pattern2Bullet.ReturnObject;
                bossSpell.BulletSpawn();
                isAttack = true;
            }
        }
        else if (normalizedTime > 1f && isAttack)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }

}

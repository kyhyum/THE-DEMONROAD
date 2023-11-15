using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossStateMachine : BossBaseStateMachine
{
    public Boss Boss { get; }

    public Transform Target {get; private set;}


    public BossIdleState IdleState { get; private set;}
    public BossChaseState ChasingState { get; private set; }
    public BossAttackState AttackState { get; private set; }
    public BossAttackOneState AttackOneState { get; private set;}
    public BossAttackTwoState AttackTwoState { get; private set;}
    public BossAttackThreeState AttackThreeState { get; private set;}
    public BossAttackFourState AttackFourState { get; private set;}


    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public BossStateMachine(Boss boss)
    {
        Boss = boss;
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        IdleState = new BossIdleState(this);
        ChasingState = new BossChaseState(this);
        AttackState = new BossAttackState(this);
        AttackOneState = new BossAttackOneState(this);
        AttackTwoState = new BossAttackTwoState(this);
        AttackThreeState = new BossAttackThreeState(this);
        AttackFourState = new BossAttackFourState(this);

        MovementSpeed = boss.Data.BaseSpeed;
        RotationDamping = boss.Data.BaseRotationDamping;
    }
}

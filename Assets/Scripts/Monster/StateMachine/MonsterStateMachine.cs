using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }

    public Transform Target {get; private set;}


    public MonsterIdleState IdleState { get; private set;}
    public MonsterChaseState ChasingState { get; private set;}
    public MonsterWalkState WalkingState { get; private set;}
    public MonsterAttackState AttackState { get; private set;}
    public MonsterGetHitState GetHitState { get; private set;}


    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; private set; }

    public MonsterStateMachine(Monster monster)
    {
        Monster = monster;
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        IdleState = new MonsterIdleState(this);
        ChasingState = new MonsterChaseState(this);
        AttackState= new MonsterAttackState(this);
        GetHitState= new MonsterGetHitState(this);
        WalkingState = new MonsterWalkState(this);

        //MovementSpeed = monster.Data.GroundedData.BaseSpeed;
        //RotationDamping = monster.Data.GroundedData.BaseRotationDamping;

    }
}

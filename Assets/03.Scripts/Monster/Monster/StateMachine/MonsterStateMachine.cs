using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterStateMachine : MonsterBaseStateMachine
{
    public Monster Monster { get; }

    public Transform Target {get; private set;}


    public MonsterIdleState IdleState { get; private set;}
    public MonsterChaseState ChasingState { get; private set;}
    public MonsterAttackState AttackState { get; private set;}
    public MonsterStunState StunState { get; private set;}


    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public MonsterStateMachine(Monster monster)
    {
        Monster = monster;
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        //TODO : Target = GameManager.Instance.Myplayer.transform;

        IdleState = new MonsterIdleState(this);
        ChasingState = new MonsterChaseState(this);
        AttackState= new MonsterAttackState(this);
        StunState= new MonsterStunState(this);

        MovementSpeed = monster.Data.BaseSpeed;
        RotationDamping = monster.Data.BaseRotationDamping;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    #region States
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }

    public PlayerAttackState AttackState { get; }
    public PlayerComboAttackState ComboAttackState { get; }
    public PlayerAttackSkill1State AttackSkill1State { get; }
    public PlayerAttackSkill2State AttackSkill2State { get; }
    public PlayerAttackSkill3State AttackSkill3State { get; }
    #endregion States

    // 
    public int ComboIndex;
    // 움직임을 받는다.
    public Vector3 MovementInput { get; set; }
    // 이동 속도이다.
    public float MovementSpeed { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine()
    {
        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);

        AttackState = new PlayerAttackState(this);
        ComboAttackState = new PlayerComboAttackState(this);
        AttackSkill1State = new PlayerAttackSkill1State(this);
        AttackSkill2State = new PlayerAttackSkill2State(this);
        AttackSkill3State = new PlayerAttackSkill3State(this);
    }
}

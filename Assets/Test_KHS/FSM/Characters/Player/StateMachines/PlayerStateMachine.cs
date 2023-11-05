using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerIdleState IdleState { get; }

    // 
    // 움직임을 받는다.
    public Vector2 MovementInput { get; set; }
    // 이동 속도이다.
    public float MovementSpeed { get; private set; }
    // 회전 댐핑값이다.
    // Damping;댐핑:진동을 흡수해서 억제시키는 것을 말한다.
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundedData.BaseSpeed;
        RotationDamping = player.Data.GroundedData.BaseRotationDamping;
    }
}

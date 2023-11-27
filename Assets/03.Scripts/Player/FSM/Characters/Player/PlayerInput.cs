using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction InputActions { get; private set; }
    // PlayerInputAction 안에 있는 PlayerActions를 쓴다.
    public PlayerInputAction.PlayerActions PlayerActions { get; private set; }

    private void Awake()
    {
        InputActions = InputManager.inputActions;

        PlayerActions = InputActions.Player;
    }

    /// <summary>
    /// InputActions을 활성화 한다.
    /// </summary>
    public void OnEnable()
    {
        InputActions.Enable();
    }

    /// <summary>
    /// InputActions을 비활성화 한다.
    /// </summary>
    public void OnDisable()
    {
        InputActions.Disable();
    }
}

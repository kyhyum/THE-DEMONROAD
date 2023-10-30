using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class InputManager : MonoBehaviour
{
    public static PlayerInputAction inputActions;
    public static event Action rebindComplete;
    public static event Action rebindCanceled;
    public static event Action<InputAction> rebindStarted;
    [SerializeField] InputActionReference[] inputActionReferences;
    private void Awake()
    {
        if (inputActions == null)
            inputActions = new PlayerInputAction();
        for (int i = 0; i < inputActionReferences.Length; i++)
        {
            LoadBindingOverride(inputActionReferences[i].action.name);
        }
    }
    public static void StartRebind(string actionName, Text statusText, bool excludeMouse)
    {
        InputAction action = inputActions.asset.FindAction(actionName);
        DoRebind(action, statusText, false, excludeMouse);
    }

    private static void DoRebind(InputAction actionToRebind, Text statusText, bool allCompositeParts, bool excludeMouse)
    {
        statusText.text = $"Press a {actionToRebind.expectedControlType}";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(0);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            SaveBindingOverride(actionToRebind);
            rebindComplete?.Invoke();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCanceled?.Invoke();
        });

        rebind.WithCancelingThrough("<Keyboard>/escape");

        if (excludeMouse)
            rebind.WithControlsExcluding("Mouse");

        rebindStarted?.Invoke(actionToRebind);
        rebind.Start(); //actually starts the rebinding process
    }

    public static string GetBindingName(string actionName)
    {
        if (inputActions == null)
            inputActions = new PlayerInputAction();

        InputAction action = inputActions.asset.FindAction(actionName);
        return action.GetBindingDisplayString();
    }

    private static void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }

    public static void LoadBindingOverride(string actionName)
    {
        if (inputActions == null)
            inputActions = new PlayerInputAction();

        InputAction action = inputActions.asset.FindAction(actionName);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
        }
    }

    public static void ResetBinding(string actionName)
    {
        InputAction action = inputActions.asset.FindAction(actionName);
            action.RemoveBindingOverride(0);

        SaveBindingOverride(action);
    }

}

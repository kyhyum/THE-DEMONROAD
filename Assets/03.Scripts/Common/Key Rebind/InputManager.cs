using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class InputManager : MonoBehaviour
{
    public static PlayerInputAction inputActions;
    public static event Action rebindComplete;
    public static event Action rebindCanceled;
    public static event Action<InputAction> rebindStarted;
    [SerializeField] InputActionReference[] inputActionReferences;
    static List<InputBinding> bindings = new List<InputBinding>();
    static InputActionRebindingExtensions.RebindingOperation beforeRebindingAction = null;

    static GameManager gameManager;
    private void Awake()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputAction();
        }
        
        for (int i = 0; i < inputActionReferences.Length; i++)
        {
            LoadBindingOverride(inputActionReferences[i].action.name);
        }
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public static void StartRebind(string actionName, Text statusText, int num)
    {
        gameManager.eventSystem.enabled = false;
        InputAction action = inputActions.asset.FindAction(actionName);
        DoRebind(action, statusText, num);
    }

    private static void DoRebind(InputAction actionToRebind, Text statusText, int num)
    {
        if(beforeRebindingAction != null)
        {
            beforeRebindingAction.Cancel();
        }

        bindings.Clear();

        foreach (var action in inputActions)
        {
            bindings.Add(action.bindings[0]);
        }

        statusText.text = $"Press a {actionToRebind.expectedControlType}";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(0);

        beforeRebindingAction = rebind;

        if (num <= 1)
        {
            rebind.WithControlsExcluding("Keyboard");
        }
        else
        {
            rebind.WithControlsExcluding("Mouse");
        }

        rebind.WithCancelingThrough("<Keyboard>/escape");

        rebindStarted?.Invoke(actionToRebind);
        rebind.Start();

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();
            SaveBindingOverride(actionToRebind);

            for(int i = 0; i < bindings.Count; i++)
            {
                if (bindings[i].overridePath != null)
                {
                    if (bindings[i].overridePath == actionToRebind.bindings[0].overridePath)
                    {
                        ResetBinding(actionToRebind.name);
                    }
                }
                else
                {
                    if (bindings[i].path == actionToRebind.bindings[0].overridePath)
                    {
                        ResetBinding(actionToRebind.name);
                    }
                }
            }
            rebindComplete?.Invoke();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCanceled?.Invoke();
        });

        gameManager.eventSystem.enabled = true;
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
        {
            inputActions = new PlayerInputAction();
        }

        InputAction action = inputActions.asset.FindAction(actionName);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
            {
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
            }
        }
    }

    public static void ResetBinding(string actionName)
    {
        InputAction action = inputActions.asset.FindAction(actionName);
        action.RemoveBindingOverride(0);

        SaveBindingOverride(action);

        foreach (var a in inputActions)
        {
            if (a.bindings[0].overridePath != null)
            {
                if (a.bindings[0].overridePath == action.bindings[0].path)
                {
                    ResetBinding(a.name);
                    return;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReBindUI : MonoBehaviour
{
    [SerializeField]
    private InputActionReference inputActionReference; //this is on the SO

    [SerializeField]
    private int num;

    private string actionName;

    [Header("UI Fields")]
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Button rebindButton;
    [SerializeField]
    private Text rebindText;
    [SerializeField]
    private Button resetButton;

    private void Awake()
    {
        UpdateKey();
    }
    private void OnEnable()
    {
        rebindButton.onClick.AddListener(() => DoRebind());
        rebindButton.onClick.AddListener(() => UpdateKey());
        resetButton.onClick.AddListener(() => ResetBinding());
        resetButton.onClick.AddListener(() => UpdateKey());

        if (inputActionReference != null)
        {
            InputManager.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }

        InputManager.rebindComplete += UpdateUI;
        InputManager.rebindCanceled += UpdateUI;
    }
    
    private void OnDisable()
    {
        InputManager.rebindComplete -= UpdateUI;
        InputManager.rebindCanceled -= UpdateUI;
    }
    private void UpdateKey()
    {
        if (inputActionReference == null)
            return;

        GetBindingInfo();
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if (inputActionReference.action != null)
        {
            actionName = inputActionReference.action.name;
        }
    }

    private void UpdateUI()
    {
        if (actionText != null)
            actionText.text = actionName;

        if(rebindText != null)
        {
            if (Application.isPlaying)
            {
                rebindText.text = InputManager.GetBindingName(actionName);
            }
            else
                rebindText.text = inputActionReference.action.GetBindingDisplayString();
        }
    }

    private void DoRebind()
    {
        InputManager.StartRebind(actionName, rebindText, num);
    }

    private void ResetBinding()
    {
        InputManager.ResetBinding(actionName);
        UpdateUI();
    }
}

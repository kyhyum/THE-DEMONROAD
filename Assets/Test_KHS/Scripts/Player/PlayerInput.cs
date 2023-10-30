using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction inputAction;

    private NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    private Vector3 hitPoint;
    public Vector3 HitPoint => hitPoint;

    float speed;

    Animator animator;

    private void Awake()
    {
        inputAction = new PlayerInputAction();

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        OnInputEnable();
    }

    private void Update()
    {
        speed = agent.velocity.magnitude;
        //print($"speed:{speed}");

        animator.SetFloat("Walk", speed);
    }

    public void OnInputEnable()
    {
        //Debug.Log("PlayerInput 클래스 OnInputEnable 함수 출력");

        inputAction.Player.Move.Enable();
        inputAction.Player.Move.started += OnInputMove;

        inputAction.Player.Attack.Enable();
        inputAction.Player.Attack.started += OnInputAttack;

        inputAction.Player.SkillUI.Enable();
        inputAction.Player.SkillUI.started += OnInputSKillUI;

        inputAction.Player.Inventory.Enable();
        inputAction.Player.Inventory.started += OnInputInventory;

        inputAction.Player.Quest.Enable();
        inputAction.Player.Quest.started += OnInputQuest;

        inputAction.Player.Map.Enable();
        inputAction.Player.Map.started += OnInputMap;

        inputAction.Player.Dodge.Enable();
        inputAction.Player.Dodge.started += OnInputDodge;

        inputAction.Player.QuickSlot1.Enable();
        inputAction.Player.QuickSlot1.started += OnInputQuickSlot1;

        inputAction.Player.QuickSlot2.Enable();
        inputAction.Player.QuickSlot2.started += OnInputQuickSlot2;

        inputAction.Player.QuickSlot3.Enable();
        inputAction.Player.QuickSlot3.started += OnInputQuickSlot3;

        inputAction.Player.QuickSlot4.Enable();
        inputAction.Player.QuickSlot4.started += OnInputQuickSlot4;

        inputAction.Player.QuickSlot5.Enable();
        inputAction.Player.QuickSlot5.started += OnInputQuickSlot5;

    }

    public void OnInputDisable()
    {
        //Debug.Log("PlayerInput 클래스 OnInputDisable 함수 출력");

        inputAction.Player.Move.Disable();
        inputAction.Player.Move.started -= OnInputMove;

        inputAction.Player.Attack.Disable();
        inputAction.Player.Attack.started -= OnInputAttack;

        inputAction.Player.SkillUI.Disable();
        inputAction.Player.SkillUI.started -= OnInputSKillUI;

        inputAction.Player.Inventory.Disable();
        inputAction.Player.Inventory.started -= OnInputInventory;

        inputAction.Player.Quest.Disable();
        inputAction.Player.Quest.started -= OnInputQuest;

        inputAction.Player.Map.Disable();
        inputAction.Player.Map.started -= OnInputMap;

        inputAction.Player.Dodge.Disable();
        inputAction.Player.Dodge.started -= OnInputDodge;

        inputAction.Player.QuickSlot1.Disable();
        inputAction.Player.QuickSlot1.started -= OnInputQuickSlot1;

        inputAction.Player.QuickSlot2.Disable();
        inputAction.Player.QuickSlot2.started -= OnInputQuickSlot2;

        inputAction.Player.QuickSlot3.Disable();
        inputAction.Player.QuickSlot3.started -= OnInputQuickSlot3;

        inputAction.Player.QuickSlot4.Disable();
        inputAction.Player.QuickSlot4.started -= OnInputQuickSlot4;

        inputAction.Player.QuickSlot5.Disable();
        inputAction.Player.QuickSlot5.started -= OnInputQuickSlot5;
    }

    private void OnInputMove(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log("PlayerInput 클래스 OnInputMove 함수 출력");

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            hitPoint = hit.point;

            agent.SetDestination(hit.point);
        }
    }

    private void OnInputAttack(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputAttack 함수 출력");
    }

    private void OnInputSKillUI(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputSKillUI 함수 출력");
    }

    private void OnInputInventory(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputInventory 함수 출력");
    }

    private void OnInputQuest(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputQuest 함수 출력");
    }

    private void OnInputMap(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputMap 함수 출력");
    }

    private void OnInputDodge(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputDodge 함수 출력");
    }

    private void OnInputQuickSlot1(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputQuickSlot1 함수 출력");
    }

    private void OnInputQuickSlot2(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputQuickSlot2 함수 출력");
    }

    private void OnInputQuickSlot3(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputQuickSlot3 함수 출력");
    }

    private void OnInputQuickSlot4(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputQuickSlot4 함수 출력");
    }

    private void OnInputQuickSlot5(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("PlayerInput 클래스 OnInputQuickSlot5 함수 출력");
    }


}


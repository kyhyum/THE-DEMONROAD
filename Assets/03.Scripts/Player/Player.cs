// 필요한 데이터들, 컴포넌트들을 사용할 것이다.

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public LayerMask groundLayerMask;


    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }

    [field: Header("Player")]
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public CapsuleCollider playerCollider { get; private set; }

    public PlayerCondition playerCondition { get; private set; }

    [field: Header("Camera")]
    [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; set; }
    [field: SerializeField] public CinemachineComponentBase ComponentBase { get; set; }
    [field: SerializeField] Transform cameraLookPoint;

    public bool IsMovePerformed { get; set; }
    public bool IsAttacking { get; set; }
    public bool IsDodging { get; set; }
    public bool[] IsAttackSkill { get; set; }

    [field: Header("Attack")]
    [field: SerializeField] public Collider WeaponCollider { get; set; }
    [field: SerializeField] public Weapon Weapon { get; private set; }
    public PlayerSO playerSO;
    [field: SerializeField] private List<Transform> AttackTransforms;
    [field: SerializeField] private List<Transform> Skill2Transforms;
    public PlayerAttackData playerBaseAttackData { get; set; }
    public PlayerAttackData playerSkill1Data { get; set; }
    public PlayerAttackData playerSkill2Data { get; set; }
    public PlayerAttackData playerSkill3Data { get; set; }
    [field: SerializeField] public AnimationEventEffects animationEventEffects { get; private set; }

    [field: Header("Skill")]
    public SkillSO[] skillSOs;
    public Skill[] skills { get; private set; }
    public AttackRange[] skillRange;

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        Agent = GetComponent<NavMeshAgent>();
        playerCollider = GetComponent<CapsuleCollider>();

        stateMachine = new PlayerStateMachine(this);

        IsAttackSkill = new bool[skillSOs.Length];
        skills = new Skill[skillSOs.Length];

        //기본 공격
        playerBaseAttackData = playerSO.AttakData[0];
        playerSkill1Data = playerSO.AttakData[1];
        playerSkill2Data = playerSO.AttakData[2];
        playerSkill3Data = playerSO.AttakData[3];
        //Attack 위치 설정
        playerBaseAttackData.SetAttackEffectTransform(AttackTransforms);
        playerSkill1Data.SetAttackEffectTransform(AttackTransforms);
        playerSkill2Data.SetAttackEffectTransform(Skill2Transforms);
        playerSkill3Data.SetAttackEffectTransform(AttackTransforms);
    }

    private void Start()
    {
        Agent.updatePosition = false;
        Agent.updateRotation = true;

        WeaponCollider.enabled = false;

        playerCondition = GetComponent<PlayerCondition>();

        Agent.speed = playerCondition.speed;

        stateMachine.ChangeState(stateMachine.IdleState);

        playerCondition.OnDie += OnDie;
        playerCondition.OnHpChanged += UIManager.Instance.playerUI.UpdateHpUI;
        playerCondition.OnMpChanged += UIManager.Instance.playerUI.UpdateMpUI;
        playerCondition.OnExpChanged += UIManager.Instance.playerUI.UpdateExpUI;

        UIManager.Instance.playerUI.UpdateHpUI(playerCondition.currentHp, playerCondition.maxHp);
        UIManager.Instance.playerUI.UpdateMpUI(playerCondition.currentMp, playerCondition.maxMp);
        UIManager.Instance.playerUI.UpdateExpUI(playerCondition.playerData.exp, playerCondition.playerData.level * 100);

        UIManager.Instance.OnUIInputEnable();

        VirtualCamera = GameManager.Instance.virtualCamera;
        if (VirtualCamera != null)
        {
            VirtualCamera.Follow = cameraLookPoint;
        }

        if (ComponentBase == null)
        {
            ComponentBase = VirtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }

        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = new AttackSkill(skillSOs[i]);
            skills[i].level = GameManager.Instance.data.skilllevels[i];
            skills[i].index = i;
        }

        UIManager.Instance.GetSkill().Init();
        UIManager.Instance.GetSkill().Set(skills);
        UIManager.Instance.SetQuickSlot(GameManager.Instance.data.QuickSlots);
    }

    private void Update()
    {
        MousePointerOverUI();

        stateMachine.Update();
    }

    /// <summary>
    /// 마우스 포인터가 UI 위에 있으면 입력을 비활성화하고, UI 위에 없으면 입력을 활성화한다.
    /// </summary>
    void MousePointerOverUI()
    {
        // 마우스 포인터가 UI 위에 있으면
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("마우스 포인터가 UI 위에 있다.");

            InputManager.inputActions.Player.Move.Disable();
            InputManager.inputActions.Player.Attack.Disable();
        }
        //
        else
        {
            //Debug.Log("마우스 포인터가 UI 위에 없다.");

            InputManager.inputActions.Player.Move.Enable();
            InputManager.inputActions.Player.Attack.Enable();
        }
    }

    private void LateUpdate()
    {
        stateMachine.LateUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        UIManager.Instance.OnUIInputDisable();
    }

    void OnDie()
    {
        Animator.SetTrigger("Die");

        UIManager.Instance.ActiveGameOver(true);

        // Player.cs를 false로 만든다.
        enabled = false;
    }

    public bool IsAttack()
    {
        bool flag = IsAttacking;

        foreach (bool b in IsAttackSkill)
        {
            flag = flag || b;
        }

        return flag;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>
{
    private PlayerInputAction inputAction;
    [field: SerializeField] private Transform canvas;
    private List<GameObject> EnableUI;

    private GameObject inventoryObject;
    private GameObject storageObject;
    private GameObject skillObject;
    private GameObject questLogObject;
    private GameObject questProgressObject;


    public GameObject settingObject;
    public GameObject playerUIObject;

    private Inventory inventory;
    private Storage storage;
    private SkillUI skill;

    private QuestLog questLog;
    private QuestProgress questProgress;

    public PlayerUI playerUI { get; private set; }

    [SerializeField] private AudioClip[] clips;
    [SerializeField] public PopUpUI popUpUI;
    private AudioSource audioSource;
    public bool storageOpen => storageObject.activeSelf;
    private Vector2 pos;

    private void Awake()
    {
        EnableUI = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();

        CreateStorage();
        CreateInventory();
        CreateSkill();
        CreateQuestLog();
        CreateQuestProgress();
    }

    private void Start()
    {
        inputAction = InputManager.inputActions;

        inputAction.Player.Escape.Enable();
        inputAction.Player.Escape.started += OnEscapeKey;

        playerUI = playerUIObject.GetComponent<PlayerUI>();

    }

    private void CreateInventory()
    {
        inventoryObject = Instantiate(Resources.Load<GameObject>(StringManager.InventoryPrefabPath), canvas);
        inventory = inventoryObject.GetComponentInChildren<Inventory>();
        inventoryObject.SetActive(false);
    }

    private void CreateStorage()
    {
        storageObject = Instantiate(Resources.Load<GameObject>(StringManager.StroagePrefabPath), canvas);
        storage = storageObject.GetComponentInChildren<Storage>();
        storageObject.SetActive(false);
    }

    private void CreateSkill()
    {
        skillObject = Instantiate(Resources.Load<GameObject>(StringManager.SKillPrefabPath), transform);
        skill = skillObject.GetComponentInChildren<SkillUI>();
        skillObject.SetActive(false);
    }

    private void CreateQuestLog()
    {
        questLogObject = Instantiate(Resources.Load<GameObject>(StringManager.QuestLogPrefabPath), canvas);
        questLog = questLogObject.GetComponentInChildren<QuestLog>();
        questLogObject.SetActive(false);
    }

    private void CreateQuestProgress()
    {
        questProgressObject = Instantiate(Resources.Load<GameObject>(StringManager.QuestProgressPath), canvas);
        questProgress = questProgressObject.GetComponentInChildren<QuestProgress>();
        questProgressObject.SetActive(false);
    }

    public void OnUIInputEnable()
    {
        inputAction.Player.Inventory.Enable();
        inputAction.Player.SkillUI.Enable();
        inputAction.Player.Quest.Enable();
        inputAction.Player.Inventory.started += ActiveInventory;
        inputAction.Player.SkillUI.started += ActiveSkill;
        inputAction.Player.Quest.started += ActiveQuestLog;
    }

    public void OnUIInputDisable()
    {
        inputAction.Player.Inventory.Disable();
        inputAction.Player.SkillUI.Disable();
        inputAction.Player.Quest.Disable();
        inputAction.Player.Inventory.started -= ActiveInventory;
        inputAction.Player.SkillUI.started -= ActiveSkill;
        inputAction.Player.Quest.started -= ActiveQuestLog;

    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public Storage GetStorage()
    {
        return storage;
    }

    public SkillUI GetSkill()
    {
        return skill;
    }

    public QuestLog GetQuestLog()
    {
        return questLog;
    }
    public QuestProgress GetQuestProgress()
    {
        return questProgress;
    }

    public void ActiveSettingWindow()
    {
        ActiveUIGameObject(settingObject);
    }

    public void ActiveInventory()
    {
        ActiveUIGameObject(inventoryObject);
    }

    private void ActiveInventory(InputAction.CallbackContext context)
    {
        ActiveInventory();
    }

    public void ActiveSkill()
    {
        ActiveUIGameObject(skillObject);
    }

    private void ActiveSkill(InputAction.CallbackContext context)
    {
        ActiveSkill();
    }

    public void ActiveQuestLog()
    {
        ActiveUIGameObject(questLogObject);
    }
    public void ActiveQuesProgress()
    {
        ActiveUIGameObject(questProgressObject);
    }

    private void ActiveQuestLog(InputAction.CallbackContext context)
    {
        ActiveQuestLog();
    }

    public void ActivePopUpUI(string title, string explan, UnityAction action)
    {
        ActiveUIGameObject(popUpUI.gameObject);
        popUpUI.OpenPopUpUI(title, explan, action);
    }

    public void ActivePlayerUI(bool flag)
    {
        playerUIObject.SetActive(flag);
    }

    public void ActivePopUpUI()
    {
        ActiveUIGameObject(popUpUI.gameObject);
    }

    public void ActiveStorage()
    {
        if (!storageObject.activeSelf)
        {
            pos = inventoryObject.GetComponent<RectTransform>().anchoredPosition;

            if (!inventoryObject.activeSelf)
            {
                ActiveUIGameObject(inventoryObject);
            }

            inventoryObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-510, 0);
            inventoryObject.GetComponentInChildren<DragAndDrop>().enabled = false;

            OnUIInputDisable();
        }
        else
        {
            inventoryObject.GetComponent<RectTransform>().anchoredPosition = pos;
            inventoryObject.GetComponentInChildren<DragAndDrop>().enabled = true;

            ActiveUIGameObject(inventoryObject);

            OnUIInputEnable();
        }

        ActiveUIGameObject(storageObject);
    }

    private void OnEscapeKey(InputAction.CallbackContext context)
    {
        if (EnableUI.Count != 0)
        {
            if (EnableUI[0].Equals(storageObject))
            {
                ActiveStorage();
            }
            else
            {
                ActiveUIGameObject(EnableUI[0]);
            }
        }
        else
        {
            ActiveSettingWindow();
        }
    }

    private void ActiveUIGameObject(GameObject gameObject)
    {
        if (EnableUI.Contains(gameObject))
        {
            EnableUI.RemoveAt(EnableUI.IndexOf(gameObject));
            gameObject.GetComponentInParent<Canvas>().sortingOrder = 40;
        }
        else
        {
            EnableUI.Insert(0, gameObject);
        }

        if (gameObject.activeSelf)
        {
            UICloseSound();
        }
        else
        {
            UIOpenSound();
        }

        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SwapItems(int slotA, int slotB)
    {
        Item item = GetInventory().GetItem(slotA);
        GetInventory().AddItem(slotA, GetStorage().GetItem(slotB));
        GetStorage().AddItem(slotB, item);
    }

    public void ClickInventory()
    {
        UIClick(inventoryObject);
    }

    public void ClickSkillUI()
    {
        UIClick(skillObject);
    }

    private void UIClick(GameObject uiObject)
    {
        if (EnableUI.Count == 1)
            return;

        if (uiObject.Equals(EnableUI[0]))
            return;

        if (!EnableUI.Contains(uiObject))
            return;

        EnableUI[0].GetComponentInParent<Canvas>().sortingOrder = 40;

        EnableUI.RemoveAt(EnableUI.IndexOf(uiObject));
        EnableUI.Insert(0, uiObject);

        uiObject.GetComponentInParent<Canvas>().sortingOrder = 50;
    }

    public void UIOpenSound()
    {
        SoundManager.Instance.SFXPlay(audioSource, clips[0]);
    }
    public void UICloseSound()
    {
        SoundManager.Instance.SFXPlay(audioSource, clips[1]);
    }
}

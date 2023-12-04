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

    private GameObject inventoryObj;
    private GameObject storageObj;
    private GameObject skillObj;
    private GameObject questLogObject;
    private GameObject questProgressObj;
    private GameObject gameOverObj;

    public GameObject settingObj;
    public GameObject playerUIObj;

    private Inventory inventory;
    private Storage storage;
    private SkillUI skill;
    public QuickSlot[] quickSlots;

    private QuestLog questLog;
    private QuestProgress questProgress;

    private GameOverUI gameOver;

    public PlayerUI playerUI { get; private set; }

    [SerializeField] private AudioClip[] clips;
    [SerializeField] public PopUpUI popUpUI;
    private AudioSource audioSource;
    public bool storageOpen => storageObj.activeSelf;
    private Vector2 pos;

    private void Awake()
    {
        quickSlots = new QuickSlot[5];
        EnableUI = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();

        CreateStorage();
        CreateInventory();
        CreateSkill();
        CreateQuestLog();
        CreateQuestProgress();
        CreateGameOver();
    }

    private void Start()
    {
        inputAction = InputManager.inputActions;

        inputAction.Player.Escape.Enable();
        inputAction.Player.Escape.started += OnEscapeKey;

        playerUI = playerUIObj.GetComponent<PlayerUI>();

    }

    private void CreateInventory()
    {
        inventoryObj = Instantiate(Resources.Load<GameObject>(StringManager.InventoryPrefabPath), canvas);
        inventory = inventoryObj.GetComponentInChildren<Inventory>();
        inventoryObj.SetActive(false);
    }

    private void CreateStorage()
    {
        storageObj = Instantiate(Resources.Load<GameObject>(StringManager.StroagePrefabPath), canvas);
        storage = storageObj.GetComponentInChildren<Storage>();
        storageObj.SetActive(false);
    }

    private void CreateSkill()
    {
        skillObj = Instantiate(Resources.Load<GameObject>(StringManager.SKillPrefabPath), transform);
        skill = skillObj.GetComponentInChildren<SkillUI>();
        skillObj.SetActive(false);
    }

    public void CreateQuestLog()
    {
        if (questLogObject != null)
        {
            return;
        }
        questLogObject = Instantiate(Resources.Load<GameObject>(StringManager.QuestLogPrefabPath), canvas);
        questLog = questLogObject.GetComponentInChildren<QuestLog>();
        questLogObject.SetActive(false);
    }

    public void CreateQuestProgress()
    {
        if (questProgressObj != null)
        {
            return;
        }
        questProgressObj = Instantiate(Resources.Load<GameObject>(StringManager.QuestProgressPath), canvas);
        questProgress = questProgressObj.GetComponentInChildren<QuestProgress>();
        questProgressObj.SetActive(false);
    }

    public void CreateGameOver()
    {
        gameOverObj = Instantiate(Resources.Load<GameObject>(StringManager.GameOverPrefabPath), canvas);
        gameOver= gameOverObj.GetComponentInChildren<GameOverUI>();
        gameOverObj.SetActive(false);
    }

    public void DestroyQuestUI()
    {
        if (questLogObject == null)
        {
            return;
        }
        Destroy(questLogObject);
        Destroy(questProgressObj);
        questLogObject = null;
        questProgressObj = null;
        questLog = null;
        questProgress = null;
    }

    public void SetQuickSlot(QuickSlotData[] data)
    {
        for (int i = 0; i < 5; i++)
        {
            switch (data[i].type)
            {
                case Define.QuickSlotType.Skill:
                    quickSlots[i].SetSlot((IUsable)skill.slots[data[i].index].GetSkill());
                    break;
                case Define.QuickSlotType.Item:
                    quickSlots[i].SetSlot((IUsable)inventory.inventorySlots[data[i].index].GetItem());
                    break;
                default:
                    break;
            }
        }
    }

    public QuickSlotData[] GetQuickSlot()
    {
        QuickSlotData[] data = new QuickSlotData[5];
        for (int i = 0; i < 5; i++)
        {
            IUsable usable = quickSlots[i].Get();

            if (usable is Skill)
            {
                Skill skill = (Skill)usable;
                data[i] = new QuickSlotData(Define.QuickSlotType.Skill, skill.index);
            }
            else if (usable is UseItem)
            {
                UseItem item = (UseItem)usable;
                data[i] = new QuickSlotData(Define.QuickSlotType.Item, inventory.FindItemIndex(item));
            }
            else
            {
                data[i] = new QuickSlotData(Define.QuickSlotType.None, 0); ;
            }
        }

        return data;
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
        ActiveUIGameObject(settingObj);
    }

    public void ActiveInventory()
    {
        ActiveUIGameObject(inventoryObj);
    }

    private void ActiveInventory(InputAction.CallbackContext context)
    {
        ActiveInventory();
    }

    public void ActiveSkill()
    {
        ActiveUIGameObject(skillObj);
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
        ActiveUIGameObject(questProgressObj);
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
        playerUIObj.SetActive(flag);
    }

    public void ActivePopUpUI()
    {
        ActiveUIGameObject(popUpUI.gameObject);
    }

    public void ActiveStorage()
    {
        if (!storageObj.activeSelf)
        {
            pos = inventoryObj.GetComponent<RectTransform>().anchoredPosition;

            if (!inventoryObj.activeSelf)
            {
                ActiveUIGameObject(inventoryObj);
            }

            inventoryObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-510, 0);
            inventoryObj.GetComponentInChildren<DragAndDrop>().enabled = false;

            OnUIInputDisable();
        }
        else
        {
            inventoryObj.GetComponent<RectTransform>().anchoredPosition = pos;
            inventoryObj.GetComponentInChildren<DragAndDrop>().enabled = true;

            ActiveUIGameObject(inventoryObj);

            OnUIInputEnable();
        }

        ActiveUIGameObject(storageObj);
    }

    private void OnEscapeKey(InputAction.CallbackContext context)
    {
        if (EnableUI.Count != 0)
        {
            if (EnableUI[0].Equals(storageObj))
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

    public void ActiveGameOver(bool flag)
    {
        gameOverObj.SetActive(flag);
    }

    public void SwapItems(int slotA, int slotB)
    {
        Item item = GetInventory().GetItem(slotA);
        GetInventory().AddItem(slotA, GetStorage().GetItem(slotB));
        GetStorage().AddItem(slotB, item);
    }

    public void ClickInventory()
    {
        UIClick(inventoryObj);
    }

    public void ClickSkillUI()
    {
        UIClick(skillObj);
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

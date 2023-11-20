using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private PlayerInputAction inputAction;
    [field: SerializeField] private Transform canvas;
    private List<GameObject> EnableUI;
    private GameObject inventoryObject;
    private GameObject storageObject;
    public GameObject settingObject;
    private Inventory inventory;
    private Storage storage;

    [SerializeField] private AudioClip[] clips;
    private AudioSource audioSource;
    private SoundManager soundManager;
    private QuickSlot[] quickSlots;
    public bool storageOpen => storageObject.activeSelf;
    private Vector2 pos;

    private void Awake()
    {
        EnableUI = new List<GameObject>();

        CreateStorage();
        CreateInventory();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }

        inputAction = InputManager.inputActions;

        inputAction.Player.Escape.Enable();
        inputAction.Player.Escape.started += OnEscapeKey;
        soundManager = SoundManager.Instance;
    }

    private void CreateInventory()
    {
        inventoryObject = Resources.Load<GameObject>("Prefabs/UI/UI_Inventory");
        inventoryObject = Instantiate(inventoryObject, canvas);
        inventory = inventoryObject.GetComponent<Inventory>();
        inventoryObject.SetActive(false);
    }

    private void CreateStorage()
    {
        storageObject = Resources.Load<GameObject>("Prefabs/UI/UI_Storage");
        storageObject = Instantiate(storageObject, canvas);
        storage = storageObject.GetComponentInChildren<Storage>();
        storageObject.SetActive(false);
    }

    public void OnUIInputEnable()
    {
        inputAction.Player.Inventory.Enable();
        inputAction.Player.SkillUI.Enable();
        inputAction.Player.QuickSlot1.Enable();
        inputAction.Player.QuickSlot2.Enable();
        inputAction.Player.QuickSlot3.Enable();
        inputAction.Player.QuickSlot4.Enable();
        inputAction.Player.QuickSlot5.Enable();
        inputAction.Player.Inventory.started += ActiveInventory;
        // inputAction.Player.QuickSlot1.started +=;
        // inputAction.Player.QuickSlot2.started +=;
        // inputAction.Player.QuickSlot3.started +=;
        // inputAction.Player.QuickSlot4.started +=;
        // inputAction.Player.QuickSlot5.started +=;
    }

    public void OnUIInputDisable()
    {
        inputAction.Player.Inventory.Disable();
        inputAction.Player.SkillUI.Disable();
        inputAction.Player.QuickSlot1.Disable();
        inputAction.Player.QuickSlot2.Disable();
        inputAction.Player.QuickSlot3.Disable();
        inputAction.Player.QuickSlot4.Disable();
        inputAction.Player.QuickSlot5.Disable();
        inputAction.Player.Inventory.started -= ActiveInventory;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public Storage GetStorage()
    {
        return storage;
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
            inventoryObject.GetComponentInChildren<InventoryDragAndDrop>().enabled = false;

            OnUIInputDisable();
        }
        else
        {
            inventoryObject.GetComponent<RectTransform>().anchoredPosition = pos;
            inventoryObject.GetComponentInChildren<InventoryDragAndDrop>().enabled = true;

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

    public void ItemAddTest(ItemSO itemSO)
    {
        OnUIInputEnable();
        Item item;

        switch (itemSO.type)
        {
            case ItemType.Equip:
                item = new EquipItem(itemSO);
                break;
            case ItemType.Consumes:
                item = new UseItem(itemSO);
                break;
            default:
                item = new ResourceItem(itemSO);
                break;
        }


        if (UIManager.Instance.GetInventory().AddItem(item))
        {
            // ����Ʈ �Ϸ�ó��
        }
        else
        {
            // �˾� ����༭ �κ��丮�� ��á���ϴ�.
            // �����ϰ� �ٽ� �Ϸ� ��ư ������
        }
    }
    public void UIOpenSound()
    {
        soundManager.SFXPlay(audioSource, clips[0]);
    }
    public void UICloseSound()
    {
        soundManager.SFXPlay(audioSource, clips[1]);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Events;
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
    public GameObject playerUIObject;

    private GameObject clickedUI;

    private Inventory inventory;
    private Storage storage;

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
        playerUI = playerUIObject.GetComponent<PlayerUI>();

        CreateStorage();
        CreateInventory();
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
        inputAction.Player.Inventory.started += ActiveInventory;
    }

    public void OnUIInputDisable()
    {
        inputAction.Player.Inventory.Disable();
        inputAction.Player.SkillUI.Disable();
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

    public void UIOpenSound()
    {
        SoundManager.Instance.SFXPlay(audioSource, clips[0]);
    }
    public void UICloseSound()
    {
        SoundManager.Instance.SFXPlay(audioSource, clips[1]);
    }
}

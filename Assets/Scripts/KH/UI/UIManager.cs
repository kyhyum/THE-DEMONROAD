using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [field: SerializeField] private Transform canvas;
    private PlayerInputAction inputAction;
    private List<GameObject> EnableUI;
    private GameObject inventoryObject;
    private GameObject storageObject;
    private Inventory inventory;
    private Inventory storage;
    [field: SerializeField] private ItemSO testItem;
    public bool storageOpen => storageObject.activeSelf;
    private Vector2 pos;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
        EnableUI = new List<GameObject>();

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

        DontDestroyOnLoad(this);
    }

    private void CreateInventory()
    {
        inventoryObject = Resources.Load<GameObject>("KH/Prefabs/UI/UI_Inventory");
        inventoryObject = Instantiate(inventoryObject, canvas);
        inventory = inventoryObject.GetComponent<Inventory>();
        inventory.SetLimit(30);
        inventoryObject.SetActive(false);
    }

    private void CreateStorage()
    {
        storageObject = Resources.Load<GameObject>("KH/Prefabs/UI/UI_Storage");
        storageObject = Instantiate(storageObject, canvas);
        storage = storageObject.GetComponent<Inventory>();
        storage.SetLimit(81);
        storageObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnUIInputEnable();
        inputAction.Player.Escape.Enable();
        inputAction.Player.Escape.started += OnEscapeKey;
    }

    public void OnUIInputEnable()
    {
        inputAction.Player.Inventory.Enable();
        inputAction.Player.Inventory.started += ActiveInventory;
    }

    public void OnUIInputDisable()
    {
        inputAction.Player.Inventory.Disable();
        inputAction.Player.Inventory.started -= ActiveInventory;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public Inventory GetStorage()
    {
        return storage;
    }

    private void ActiveInventory(InputAction.CallbackContext context)
    {
        ActiveUIGameObject(inventoryObject);
    }

    public void TestMethodMakeItem()
    {
        GameObject item = testItem.CreateItem();
    }

    private void ActiveStorage()
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

        gameObject.SetActive(!gameObject.activeSelf);
    }
}

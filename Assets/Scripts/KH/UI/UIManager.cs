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
    private Storage storage;
    [field: SerializeField] private ItemSO testItem;
    public bool storageOpen => storageObject.activeSelf;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
        EnableUI = new List<GameObject>();

        CreateInventory();
        CreateStorage();
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
        inventoryObject.SetActive(false);
    }

    private void CreateStorage()
    {
        storageObject = Resources.Load<GameObject>("KH/Prefabs/UI/UI_Storage");
        storageObject = Instantiate(storageObject, canvas);
        storage = storageObject.GetComponent<Storage>();
        storageObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnUIInputEnable();
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

    private void ActiveInventory(InputAction.CallbackContext context)
    {
        Debug.Log("Active Inventory");

        if (!inventoryObject.activeSelf)
        {
            EnableUI.Insert(0, inventoryObject);
        }
        else
        {
            EnableUI.RemoveAt(EnableUI.IndexOf(inventoryObject));
        }

        inventoryObject.SetActive(!inventoryObject.activeSelf);
    }

    public void TestMethodMakeItem()
    {
        GameObject item = testItem.CreateItem();
    }

    public void ActiveStorage()
    {
        if (!storageObject.activeSelf)
        {
            if (!inventoryObject.activeSelf)
            {
                EnableUI.Add(inventoryObject);
                inventoryObject.SetActive(true);
                inventoryObject.GetComponentInChildren<InventoryDragAndDrop>().enabled = false;
            }

            EnableUI.Insert(0, storageObject);
            storageObject.SetActive(true);
        }
        else
        {
            EnableUI.RemoveAt(EnableUI.IndexOf(storageObject));
            inventoryObject.GetComponentInChildren<InventoryDragAndDrop>().enabled = true;
        }


        storageObject.SetActive(!storageObject.activeSelf);
    }
}

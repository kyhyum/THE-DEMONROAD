using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [field: SerializeField] private Transform canvas;
    private PlayerInputAction inputAction;
    private List<GameObject> EnableUI;
    private GameObject inventoryObject;
    private Inventory inventory;
    [field: SerializeField] private ItemSO testItem;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
        EnableUI = new List<GameObject>();

        inventoryObject = Resources.Load<GameObject>("KH/Prefabs/UI/UI_Inventory");
        inventoryObject = Instantiate(inventoryObject, canvas);
        inventory = inventoryObject.GetComponent<Inventory>();
        inventoryObject.SetActive(false);
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

    private void OnEnable()
    {
        OnUIInputEnable();
    }

    public void OnUIInputEnable()
    {
        Debug.Log("Enable UI Input");

        inputAction.Player.Inventory.Enable();
        inputAction.Player.Inventory.started += ActiveInventory;
    }

    public void OnUIInputDisable()
    {
        Debug.Log("Disable UI Input");

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
            EnableUI.Add(inventoryObject);
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
}

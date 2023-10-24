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
    private GameObject Inventory;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
        EnableUI = new List<GameObject>();

        Inventory = Resources.Load<GameObject>("KH/Prefabs/UI/Inventory");
        Inventory = Instantiate(Inventory, canvas);
        Inventory.SetActive(false);
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


    private void ActiveInventory(InputAction.CallbackContext context)
    {
        Debug.Log("Active Inventory");

        if (!Inventory.activeSelf)
        {
            EnableUI.Add(Inventory);
        }
        else
        {
            EnableUI.RemoveAt(EnableUI.IndexOf(Inventory));
        }

        Inventory.SetActive(!Inventory.activeSelf);
    }
}

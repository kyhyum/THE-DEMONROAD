using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject potionShop;
    public GameObject potionNPCtalk;
    public GameObject potionInteractionPop;
    public GameObject confirmationPopUp;
    public TMP_Text confirmationText;
    public Button confirmationButton;
    ItemSO item;
    
    

    public void Start()
    {
        potionShop.SetActive(false);
        confirmationPopUp.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShopUI();
        }
    }

    public void OnItemClick(ItemSO clickedItem)
    {
        item = clickedItem;

        if(item != null)
        {
            confirmationPopUp.SetActive(true);
            confirmationText.text = "구매하시겠습니까? "+"\n" + item.itemName + " - " + item.itemPrice + " Gold";

            confirmationButton.onClick.AddListener(BuyItem);

        }
        else
        {
            Debug.Log("Item이 null입니다.");
        }
        
    }
    public void BuyItem()
    {
        Inventory inventory = UIManager.Instance.GetInventory();

        if (inventory != null)
        {
            if (inventory.Gold >= item.itemPrice)
            {
                // 인벤토리에 아이템 추가
                ItemSO itemSO = Resources.Load<ItemSO>(item.itemName);
                Item itemToAdd = new Item(itemSO);
                inventory.AddItem(itemToAdd);

                // 골드 차감
                inventory.Gold -= item.itemPrice;
            }
            else
            {
                Debug.Log("골드가 부족합니다.");
            }
        }
        else
        {
            Debug.Log("Inventory가 null입니다.");
        }
        confirmationPopUp.SetActive(false);
    }

    

   

    public void OpenShopUI()
    {
        potionShop.SetActive(true);
        potionNPCtalk.SetActive(false);
        potionInteractionPop.SetActive(false);
        UIManager.Instance.ActiveInventory();
    }

    private void CloseShopUI()
    {
        potionShop.SetActive(false);
        confirmationPopUp.SetActive(false);
    }

}

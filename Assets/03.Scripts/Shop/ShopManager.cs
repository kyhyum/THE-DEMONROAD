using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopManager : MonoBehaviour
{
    //potionShop
    public GameObject potionShop;
    public GameObject potionNPCtalk;
    

    //EquipShop
    public GameObject equipShop;
    public GameObject equipNPCtalk;

    private bool isPotionShopOpen = false;
    private bool isEquipShopOpen = false;

    public GameObject confirmationPopUp;
    public GameObject outofGoldPop;

    public TMP_Text confirmationText;
    public TMP_Text itemCountText;

    public Button confirmationButton;
    public Button increaseButton; 
    public Button decreaseButton;

    private int itemCountToBuy = 1;
    
    ItemSO item;
    
    

    public void Start()
    {
        potionShop.SetActive(false);
        equipShop.SetActive(false);

        confirmationPopUp.SetActive(false);
        outofGoldPop.SetActive(false);

        increaseButton.onClick.AddListener(IncreaseItemCount);
        decreaseButton.onClick.AddListener(DecreaseItemCount);
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

        if (item != null)
        {
            confirmationPopUp.SetActive(true);
            confirmationText.text = "구매하시겠습니까? " + "\n" + item.itemName; 
                                     

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

        if (inventory != null && item != null)
        {
            int totalPrice = item.itemPrice * itemCountToBuy;

            if (inventory.Gold >= totalPrice)
            {
                for (int i = 0; i < itemCountToBuy; i++)
                {
                    ItemSO itemSO = Resources.Load<ItemSO>(item.itemName);
                    Item itemToAdd = new Item(itemSO);
                    inventory.AddItem(itemToAdd);
                }

                // 골드 차감
                inventory.Gold -= totalPrice;
            }
            else
            {
                Debug.Log("골드가 부족합니다.");
                outofGoldPop.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Inventory가 null입니다.");
        }
        confirmationPopUp.SetActive(false);
    }
    public void IncreaseItemCount()
    {
        
        itemCountToBuy++;
        UpdateItemCount();
    }
    public void DecreaseItemCount()
    {
        
        itemCountToBuy = Mathf.Max(itemCountToBuy - 1, 1);
        UpdateItemCount();
    }

    private void UpdateItemCount()
    {
        if(itemCountText != null)
        {
            itemCountText.text = itemCountToBuy.ToString() + " 개";
            
        }
       
    }


    public void ClosePop()
    {
        outofGoldPop.SetActive(false);
    }    
   

    public void OpenShopUI()
    {
        potionShop.SetActive(true);
        potionNPCtalk.SetActive(false);
        
        isPotionShopOpen = true;
        isEquipShopOpen = false;

        UIManager.Instance.ActiveInventory();
    }
    public void OpenEquipShopUI()
    {
        equipShop.SetActive(true);
        equipNPCtalk.SetActive(false);

        isEquipShopOpen = true;
        isPotionShopOpen = false;

        UIManager.Instance.ActiveInventory();
    }

    private void CloseShopUI()
    {
        if (isPotionShopOpen) 
        {
            potionShop.SetActive(false);
        }
        else if(isEquipShopOpen) 
        {
            equipShop.SetActive(false);
        }
        confirmationPopUp.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopManager : MonoBehaviour
{
    public GameObject potionShop;
    public GameObject potionNPCtalk;
    public GameObject potionInteractionPop;
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
        // 아이템 갯수를 증가시키는 함수
        itemCountToBuy++;
        UpdateItemCount();
    }
    public void DecreaseItemCount()
    {
        // 아이템 갯수를 감소시키는 함수
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
        potionInteractionPop.SetActive(false);
        UIManager.Instance.ActiveInventory();
    }

    private void CloseShopUI()
    {
        potionShop.SetActive(false);
        confirmationPopUp.SetActive(false);
    }

}

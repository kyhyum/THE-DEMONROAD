using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class ShopManager : Singleton<ShopManager>
{
    //potionShop
    public GameObject potionShop;
    public GameObject potionNPCtalk;
    

    //EquipShop
    public GameObject equipShop;
    public GameObject equipNPCtalk;

    //bool
    private bool isPotionShopOpen = false;
    private bool isEquipShopOpen = false;

    //Pop
    public GameObject confirmationPopUp;
    public GameObject sellconfirmationPopup;
    public GameObject equipsellconfirmationPopUp;
    public GameObject outofGoldPop;

    //Text
    public TMP_Text confirmationText;
    
    public TMP_Text itemCountText;

    //Button
    public Button confirmationButton;
    
    public Button increaseButton; 
    public Button decreaseButton;

    private int itemCountToBuy = 1;
    
    ItemSO item;
    public List<ItemSO> itemList;



    public void Start()
    {
        potionShop.SetActive(false);
        equipShop.SetActive(false);

        confirmationPopUp.SetActive(false);
        sellconfirmationPopup.SetActive(false);
        equipsellconfirmationPopUp.SetActive(false);
        outofGoldPop.SetActive(false);

        increaseButton.onClick.AddListener(IncreaseItemCount);
        decreaseButton.onClick.AddListener(DecreaseItemCount);

        
        confirmationButton.onClick.AddListener(BuyItem);

        
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
            ItemSO foundItemSO = null;

            
            foreach (ItemSO itemSO in itemList)
            {
                if (itemSO.itemName == item.itemName)
                {
                    foundItemSO = itemSO;
                    break; 
                }
            }

            if (foundItemSO != null)
            {
                if (inventory.Gold >= totalPrice)
                {
                    for (int i = 0; i < itemCountToBuy; i++)
                    {
                        Item itemToAdd = new Item(foundItemSO);
                        inventory.AddItem(itemToAdd);
                    }

                    
                    inventory.Gold -= totalPrice;
                    confirmationPopUp.SetActive(false);
                }
                else
                {
                    Debug.Log("골드가 부족합니다.");
                    outofGoldPop.SetActive(true);
                    confirmationPopUp.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("해당 아이템을 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.Log("Inventory가 null입니다.");
        }
    }
    public void SellItem()
    {
        Inventory inventory = UIManager.Instance.GetInventory();

        if (inventory != null && item != null)
        {
            int sellPrice = item.itemPrice; 

            
            inventory.Gold += sellPrice;

            sellconfirmationPopup.SetActive(false);
        }
        else
        {
            Debug.LogWarning("인벤토리가 없거나 판매할 아이템이 선택되지 않았습니다.");
        }
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

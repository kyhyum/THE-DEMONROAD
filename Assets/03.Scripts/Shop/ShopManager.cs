using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
    public GameObject sellconfirmationPopup;
    public GameObject outofGoldPop;

    public TMP_Text confirmationText;
    public TMP_Text sellconfirmationText;
    public TMP_Text itemCountText;

    public Button confirmationButton;
    public Button sellButton;
    public Button increaseButton; 
    public Button decreaseButton;

    private int itemCountToBuy = 1;
    
    ItemSO item;
    
    

    public void Start()
    {
        potionShop.SetActive(false);
        equipShop.SetActive(false);

        confirmationPopUp.SetActive(false);
        sellconfirmationPopup.SetActive(false);
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
                                     
            //아이템 구매버튼 이벤트 설정
            confirmationButton.onClick.AddListener(BuyItem);

            //아이템 판매버튼 이벤트
            //sellconfirmationText.text = "판매하시겠습니까? " + "\n" + item.itemName;

            //sellButton.onClick.RemoveAllListeners(); 
            //sellButton.onClick.AddListener(() => SellItem(item));

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
            Debug.Log("Inventory가 null입니다.");
        }
        
    }
    //public void SellItem(ItemSO soldItem)
    //{
    //    Inventory inventory = UIManager.Instance.GetInventory();

    //    if (inventory != null && soldItem != null)
    //    {
    //        // 판매할 아이템이 인벤토리에 있는지 확인
    //        for (int i = 0; i < inventory.inventorySlots.Length; i++)
    //        {
    //            Item currentItem = inventory.GetItem(i);

                
    //            if (currentItem != null && currentItem.itemName == soldItem.itemName)
    //            {
                    
    //                inventory.inventorySlots[i].Clear();
    //                inventory.Gold += soldItem.itemPrice;

    //                Debug.Log(soldItem.itemName + "을(를) 판매하였습니다. 금화 +" + soldItem.itemPrice);
    //                return; 
    //            }
    //        }

    //        Debug.Log(soldItem.itemName + "을(를) 인벤토리에서 찾을 수 없습니다.");
    //    }
    //    else
    //    {
    //        Debug.Log("Inventory가 null이거나 판매할 아이템이 null입니다.");
    //    }
    //}
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

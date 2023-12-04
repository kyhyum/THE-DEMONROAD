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
    public GameObject sellConfirmationPopup;
    public GameObject outofGoldPop;
    public GameObject outofSpace;

    //Text
    public TMP_Text confirmationText;

    public TMP_Text itemCountText;
    public TMP_Text sellitemCountText;

    //Button
    public Button confirmationButton;

    public Button increaseButton;
    public Button decreaseButton;
    public Button sellincreaseCountBtn;
    public Button selldecreaseCountBtn;

    private int itemCountToBuy = 1;

    Shop shop;
    ItemSO item;
    public List<ItemSO> itemList;



    public void Start()
    {
        potionShop.SetActive(false);
        equipShop.SetActive(false);

        confirmationPopUp.SetActive(false);
        sellConfirmationPopup.SetActive(false);
        outofGoldPop.SetActive(false);
        outofSpace.SetActive(false);

        increaseButton.onClick.AddListener(IncreaseItemCount);
        decreaseButton.onClick.AddListener(DecreaseItemCount);
        sellincreaseCountBtn.onClick.AddListener(IncreaseSellItemCount);
        selldecreaseCountBtn.onClick.AddListener(DecreaseSellItemCount);

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
            if (inventory.GetSize() == 30)
            {
                
                outofSpace.SetActive(true);
                return;
            }

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
                    switch (foundItemSO.type)
                    {
                        case Define.ItemType.Equip:
                            for (int i = 0; i < itemCountToBuy; i++)
                            {
                                EquipItem itemToAdd = new EquipItem(foundItemSO);
                                inventory.AddItem(itemToAdd);
                            }
                            break;
                        case Define.ItemType.Consumes:
                            if (foundItemSO is BuffItemSO)
                            {
                                BuffItem itemToAdd = new BuffItem(foundItemSO);
                                itemToAdd.Add(itemCountToBuy);
                                inventory.AddItem(itemToAdd);
                            }
                            else if (foundItemSO is RestoreItemSO)
                            {
                                RestoreItem itemToAdd = new RestoreItem(foundItemSO);
                                itemToAdd.Add(itemCountToBuy);
                                inventory.AddItem(itemToAdd);
                            }
                            break;
                        case Define.ItemType.Resources:
                            break;
                        default:
                            break;
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
        itemCountToBuy = 1;
        UpdateItemCount();
    }
    public void SellItem(int slotIndex)
    {
        Inventory inventory = UIManager.Instance.GetInventory();
        Item itemToSell = inventory.GetItem(slotIndex);


        if (itemToSell != null)
        {
            string itemToSellName = itemToSell.itemName;

            foreach (ItemSO itemSO in itemList)
            {
                if (itemSO.itemName == itemToSellName)
                {
                    int sellPrice = itemSO.itemPrice;

                    inventory.Gold += sellPrice *itemCountToBuy;
                    inventory.RemoveItem(slotIndex);

                    Debug.Log(itemToSellName + "을(를) 판매했습니다. 획득한 골드: " + sellPrice);

                    if (sellConfirmationPopup != null)
                    {
                        sellConfirmationPopup.SetActive(false);
                    }
                    else
                    {
                        Debug.LogError("sellConfirmationPopup이 null입니다.");
                    }
                    return;
                }
            }


            Debug.LogError("판매하려는 아이템의 정보를 찾을 수 없습니다.");
        }
        else
        {
            Debug.LogError("해당 슬롯에 아이템이 없습니다.");
        }
        itemCountToBuy = 1;
        UpdateItemCount();

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
        if (itemCountText != null)
        {
            itemCountText.text = itemCountToBuy.ToString() + " 개";

        }

    }
    public void IncreaseSellItemCount()
    {
        itemCountToBuy++;
        UpdateSellItemCount();
    }
    public void DecreaseSellItemCount()
    {

        itemCountToBuy = Mathf.Max(itemCountToBuy - 1, 1);
        UpdateSellItemCount();
    }
    private void UpdateSellItemCount()
    {
        if(sellitemCountText != null)
        {
            sellitemCountText.text = itemCountToBuy.ToString() + " 개";
        }
    }


    public void CloseOutofGoldPop()
    {
        outofGoldPop.SetActive(false);
    }
    public void CloseOutofSpacePop()
    {
        outofSpace.SetActive(false);
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
        else if (isEquipShopOpen)
        {
            equipShop.SetActive(false);
        }
        confirmationPopUp.SetActive(false);
    }

}

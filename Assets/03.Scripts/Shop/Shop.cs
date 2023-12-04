using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject sellConfirmationPopup;
    public TMP_Text sellConfirmationText;
    public Button sellConfirmButton;

    private void Start()
    {
        sellConfirmationPopup.SetActive(false);
    }

    public void SellItem()
    {
        int slotIndex = 0; 

        Inventory inventory = UIManager.Instance.GetInventory(); 
        if (inventory != null)
        {
            ShopManager.Instance.SellItem(slotIndex);
        }
        else
        {
            Debug.LogError("Inventory 스크립트를 가져올 수 없습니다.");
        }

    }
    public void ShowConfirmationPop()
    {
        int slotIndex = 0; 

        Inventory inventory = UIManager.Instance.GetInventory();
        if (inventory != null)
        {
            Item itemToSell = inventory.GetItem(slotIndex);
            if (itemToSell != null)
            {
                string itemName = itemToSell.itemName;
                int sellPrice = itemToSell.itemPrice; 

                sellConfirmationText.text = itemName + "을(를) 판매하시겠습니까?";
                sellConfirmationPopup.SetActive(true);
            }
            else
            {
                Debug.LogError("해당 슬롯에 아이템이 없습니다.");
            }
        }
        else
        {
            Debug.LogError("Inventory 스크립트를 가져올 수 없습니다.");
        }
    }
}

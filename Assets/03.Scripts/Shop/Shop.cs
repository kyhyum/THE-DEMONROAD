using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject sellConfirmationPopup;
    public TMP_Text sellConfirmationText;
    public Button sellConfirmButton;

    private ItemSO currentItem;

    
    public void SellItem()
    {
        ShopManager.Instance.SellItem();
        
    }
   

    
    public void ShowSellConfirmationPopup(ItemSO item)
    {
        currentItem = item; 

        
        sellConfirmationPopup.SetActive(true);
        sellConfirmationText.text = item.itemName + "아이템을 판매하시겠습니까?"; 
    }
}

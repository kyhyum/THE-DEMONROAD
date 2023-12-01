using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemTestScript : MonoBehaviour
{
    public void ItemAddTest(ItemSO itemSO)
    {
        UIManager.Instance.OnUIInputEnable();
        Item item;

        switch (itemSO.type)
        {
            case Define.ItemType.Equip:
                item = new EquipItem(itemSO);
                break;
            case Define.ItemType.Consumes:
                item = new UseItem(itemSO);
                break;
            default:
                item = new ResourceItem(itemSO);
                break;
        }


        if (UIManager.Instance.GetInventory().AddItem(item))
        {
            // 퀘스트 완료처리
        }
        else
        {
            // 팝업 띄워줘서 인벤토리가 꽉찼습니다.
            // 정리하고 다시 완료 버튼 누르게
        }
    }

}
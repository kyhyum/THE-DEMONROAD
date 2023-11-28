using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void CloseBtn()
    {
        if (UIManager.Instance.storageOpen)
        {
            UIManager.Instance.ActiveStorage();
        }
        else
        {
            UIManager.Instance.ActiveInventory();
        }
    }
}

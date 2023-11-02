using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupcontroller : MonoBehaviour
{
    public GameObject popup;
   public void ClosePopUp()
    {
        popup.SetActive(false);
    }

}

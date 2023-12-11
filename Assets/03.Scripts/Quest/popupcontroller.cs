using UnityEngine;

public class Popupcontroller : MonoBehaviour
{
    public GameObject popup;
   public void ClosePopUp()
    {
        popup.SetActive(false);
    }

}

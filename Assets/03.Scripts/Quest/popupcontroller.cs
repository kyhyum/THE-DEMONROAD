using UnityEngine;

public class popupcontroller : MonoBehaviour
{
    public GameObject popup;
   public void ClosePopUp()
    {
        popup.SetActive(false);
    }

}

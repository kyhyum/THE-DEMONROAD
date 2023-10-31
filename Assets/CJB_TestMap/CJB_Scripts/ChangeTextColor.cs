
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text textToChange;
    private Color originalColor;
    void Start()
    {
        originalColor = textToChange.color;    
    }
    public void OnButtonClick()
    {
        textToChange.color = Color.red;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        textToChange.color = Color.yellow;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        textToChange.color = originalColor;
    }
}

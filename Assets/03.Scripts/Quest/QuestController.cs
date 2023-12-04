using UnityEngine;
using System.Collections;

public class QuestController : MonoBehaviour
{
    
    public GameObject completeUI; 

    public float fadeDuration = 1f; 

    

    private void Start()
    {
        

        if (completeUI.activeSelf)
        {
            completeUI.SetActive(false); 
        }
    }
    
    
    public void ShowPopup()
    {
        StartCoroutine(FadeIn());
    }

    public void HidePopup()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        completeUI.SetActive(true); 
        CanvasGroup canvasGroup = completeUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        CanvasGroup canvasGroup = completeUI.GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        completeUI.SetActive(false); 
    }
}

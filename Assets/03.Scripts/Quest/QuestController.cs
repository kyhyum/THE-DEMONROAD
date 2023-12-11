using UnityEngine;
using System.Collections;

public class QuestController : MonoBehaviour
{
    
    public GameObject completeUI; 

    public float fadeDuration = 1f;

    private Coroutine fadeInCoroutine;
    private Coroutine fadeOutCoroutine;
    

    private void Start()
    {
        if (completeUI != null && completeUI.activeSelf)
        {
            completeUI.SetActive(false);
        }
    }
    
    
    public void ShowPopup()
    {
        if (gameObject.activeSelf && fadeInCoroutine == null)
        {
            fadeInCoroutine = StartCoroutine(FadeIn());
        }
    }

    public void HidePopup()
    {
        if (gameObject.activeSelf && fadeOutCoroutine == null)
        {
            fadeOutCoroutine = StartCoroutine(FadeOut());
        }
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

        fadeInCoroutine = null;
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
        fadeOutCoroutine = null;
    }
}

using UnityEngine;
using System.Collections;

public class QuestController : MonoBehaviour
{
    
    public GameObject completeUI; 

    public float fadeDuration = 1f; 

    

    private void Start()
    {
        

        if (gameObject.activeSelf && completeUI != null && completeUI.activeSelf)
        {
            completeUI.SetActive(false); 
        }
    }
    
    
    public void ShowPopup()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(FadeIn());
        }
    }

    public void HidePopup()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        if (gameObject.activeSelf)
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
        else
        {
            Debug.LogWarning("QuestController is inactive. Cannot start coroutine.");
        }
    }

    IEnumerator FadeOut()
    {
        if (gameObject.activeSelf)
        {
            CanvasGroup canvasGroup = completeUI.GetComponent<CanvasGroup>();

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeDuration;
                yield return null;
            }

            completeUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("QuestController is inactive. Cannot start coroutine.");
        }
    }
}

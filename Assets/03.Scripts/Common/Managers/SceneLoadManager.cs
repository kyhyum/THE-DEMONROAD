using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    public static string nextSceneName = string.Empty;
    public static int nextSceneNumber = -1;
    [SerializeField] Image progressBar;
    [SerializeField] Image pivot;
    [SerializeField] TMP_Text barText;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    public static void LoadScene(int sceneNumber)
    {
        nextSceneNumber = sceneNumber;
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op;

        if (string.Empty.Equals(nextSceneName))
        {
            op = SceneManager.LoadSceneAsync(nextSceneNumber);
        }
        else
        {
            op = SceneManager.LoadSceneAsync(nextSceneName);

        }

        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                barText.text = string.Format("{0:F1}%", progressBar.fillAmount * 100);
                pivot.rectTransform.anchoredPosition = new Vector2(1370 * progressBar.fillAmount, 0);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                barText.text = string.Format("{0:F1}%", progressBar.fillAmount * 100);
                pivot.rectTransform.anchoredPosition = new Vector2(1370 * progressBar.fillAmount, 0);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

        nextSceneName = string.Empty;
        nextSceneNumber = -1;
    }
}

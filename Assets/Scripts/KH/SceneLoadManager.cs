using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    public static string nextSceneName = string.Empty;
    public static int nextSceneNumber = -1;
    [SerializeField] Slider progressBar;

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
            op = SceneManager.LoadSceneAsync(nextSceneName);

        }
        else
        {
            op = SceneManager.LoadSceneAsync(nextSceneNumber);
        }

        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                if (progressBar.value == 1.0f)
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

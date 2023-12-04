using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        yield return new WaitForSeconds(1.0f);

        UIManager.Instance.ActiveGameOver(false);

        // 씬 마을로 이동
        SceneLoadManager.LoadScene(3);

        GameManager.Instance.player.transform.position = Vector3.zero;
    }
}

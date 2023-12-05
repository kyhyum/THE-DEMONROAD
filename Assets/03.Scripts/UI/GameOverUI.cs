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
        yield return new WaitForSeconds(3f);

        UIManager.Instance.ActiveGameOver(false);

        SceneLoadManager.LoadScene(3);

        GameManager.Instance.player.transform.position = Vector3.zero;
    }
}

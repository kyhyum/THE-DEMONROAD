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

        GameManager.Instance.data.currentPlayerPos = Vector3.zero;

        SceneLoadManager.LoadScene(3);

        GameManager.Instance.player.enabled = true;
        GameManager.Instance.player.Input.enabled = true;

        GameManager.Instance.condition.currentHp = GameManager.Instance.condition.maxHp;
        UIManager.Instance.playerUI.UpdateHpUI(GameManager.Instance.condition.currentHp, GameManager.Instance.condition.maxHp);
    }
}

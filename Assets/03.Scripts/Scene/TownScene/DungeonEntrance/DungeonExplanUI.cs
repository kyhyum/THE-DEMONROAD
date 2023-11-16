using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DungeonExplanUI : MonoBehaviour
{
    SceneType dungeon;
    [SerializeField] Image image;
    [SerializeField] TMP_Text dungeonName;
    [SerializeField] TMP_Text dungeonExplan;
    [SerializeField] TMP_Text dungeonApproLevel;

    public void OpenExplan(DungeonSO dungeonSO)
    {
        gameObject.SetActive(true);
        dungeon = dungeonSO.dungeon;
        image.sprite = dungeonSO.sprite;
        dungeonName.text= dungeonSO.name;
        dungeonExplan.text = dungeonSO.explan;
        dungeonApproLevel.text = dungeonSO.appropriateLevel;
    }
    public void InDungeon()
    {
        GameManager.Instance.player.currentPlayerPos = Vector3.zero;
        SceneLoadManager.LoadScene((int)dungeon);
    }
}

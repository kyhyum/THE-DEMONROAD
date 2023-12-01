using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dungeon", menuName = "Create/Dungeon", order = 0)]
public class DungeonSO : ScriptableObject
{
    public Sprite sprite;
    public string dungeonName;
    public string explan;
    public string appropriateLevel;
    public Define.SceneType dungeon;
}

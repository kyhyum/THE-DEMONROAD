using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public int exp;
    public Define.Job job;
    public List<Stat> stats;
    public int skillPoint;
    public int[] skilllevels;
    public string baseObjectPath;
    public int playerIndex;
    public IUsable[] QuickSlots;

    public Define.SceneType scene;
    public Vector3 currentPlayerPos;
    public Quaternion currentPlayerRot;

    public bool isDead;

    public List<QuestSO> acceptQuest;
}

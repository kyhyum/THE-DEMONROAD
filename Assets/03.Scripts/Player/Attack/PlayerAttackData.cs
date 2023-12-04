using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfoData
{
    [field: SerializeField] public string AttackInfoName { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }

    [field: SerializeField] public int Damage { get; private set; }
}


[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    [field: SerializeField] public AttackEffectInfoData[] Effects { get; private set; }
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfoData GetAttackInfo(int index) { return AttackInfoDatas[index]; }

    public void SetAttackEffectTransform(List<Transform> transforms)
    {
        for(int i = 0; i < Effects.Length; i++)
        {
            Effects[i].StartPositionRotation = transforms[Effects[i].EffectNum];
        }
    }

}

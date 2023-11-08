using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
// 공격에 관한 데이터를 가지고 있을 것이다.
public class AttackInfoData
{
    // 공격의 이름이다.
    [field: SerializeField] public string AttackName { get; private set; }

    // 데미지이다.
    [field: SerializeField] public int Damage { get; private set; }
}

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public AttackInfoData AttackInfo { get; private set; }
}

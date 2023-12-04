using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackEffectInfoData
{
    public GameObject Effect;
    public int EffectNum;
    public Transform StartPositionRotation;
    public float DestroyAfter = 10;
    public bool UseLocalPosition = true;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string chaseParameterName = "Chase";
    [SerializeField] private string getHitParameterName = "Gethit";

    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }
    public int GetHitParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        ChaseParameterHash = Animator.StringToHash(chaseParameterName);
        GetHitParameterHash = Animator.StringToHash(getHitParameterName);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [field: SerializeField] private string idleParameterName = "Idle";
    [field: SerializeField] private string walkParameterName = "Walk";

    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
    }
}

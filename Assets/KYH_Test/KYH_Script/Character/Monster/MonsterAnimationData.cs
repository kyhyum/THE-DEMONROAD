using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string chaseParameterName = "Chase";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string stunParameterName = "Stun";

    public int IdleParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int StunParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        ChaseParameterHash = Animator.StringToHash(chaseParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        StunParameterHash = Animator.StringToHash(stunParameterName);
    }
}

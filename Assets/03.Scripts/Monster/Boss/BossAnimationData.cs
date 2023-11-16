using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BossAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string chaseParameterName = "Chase";
    [SerializeField] private string attack1ParameterName = "Attack1";
    [SerializeField] private string attack2ParameterName = "Attack2";
    [SerializeField] private string attack3ParameterName = "Attack3";
    [SerializeField] private string attack4ParameterName = "Attack4";

    public int IdleParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }
    public int Attack1ParameterHash { get; private set; }
    public int Attack2ParameterHash { get; private set; }
    public int Attack3ParameterHash { get; private set; }
    public int Attack4ParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        ChaseParameterHash = Animator.StringToHash(chaseParameterName);
        Attack1ParameterHash = Animator.StringToHash(attack1ParameterName);
        Attack2ParameterHash = Animator.StringToHash(attack2ParameterName);
        Attack3ParameterHash = Animator.StringToHash(attack3ParameterName);
        Attack4ParameterHash = Animator.StringToHash(attack4ParameterName);
    }
}

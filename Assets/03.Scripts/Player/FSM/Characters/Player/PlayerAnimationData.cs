using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string attackSkill1ParameterName = "AttackSkill1";
    [SerializeField] private string attackSkill2ParameterName = "AttackSkill2";
    [SerializeField] private string attackSkill3ParameterName = "AttackSkill3";

    #region 프로퍼티
    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int AttackSkill1ParameterHash { get; private set; }
    public int AttackSkill2ParameterHash { get; private set; }
    public int AttackSkill3ParameterHash { get; private set; }
    #endregion 프로퍼티

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        AttackSkill1ParameterHash = Animator.StringToHash(attackSkill1ParameterName);
        AttackSkill2ParameterHash = Animator.StringToHash(attackSkill2ParameterName);
        AttackSkill3ParameterHash = Animator.StringToHash(attackSkill3ParameterName);
    }
}

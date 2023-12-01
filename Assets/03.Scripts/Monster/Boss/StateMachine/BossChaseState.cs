using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class BossChaseState : BossBaseState
{
    protected BossAttackPattern previousAttackPattern;

    bool isAttackPattern2 = false;
    bool isAttackPattern4 = false;
    public BossChaseState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        previousAttackPattern = GetRandomAttackPattern();
        StartAnimation(stateMachine.Boss.bossAnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Boss.bossAnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        base.Update();
        stateMachine.Boss.BossNavMeshAgent.SetDestination(stateMachine.Target.transform.position);
        if (IsInAttackRange())
        {
            stateMachine.Boss.BossNavMeshAgent.ResetPath();
            switch (previousAttackPattern)
            {
                case BossAttackPattern.Pattern1:
                    stateMachine.ChangeState(stateMachine.AttackOneState);
                    break;
                case BossAttackPattern.Pattern2:
                    stateMachine.ChangeState(stateMachine.AttackTwoState);
                    break;
                case BossAttackPattern.Pattern3:
                    stateMachine.ChangeState(stateMachine.AttackThreeState);
                    break;
                case BossAttackPattern.Pattern4:
                    stateMachine.ChangeState(stateMachine.AttackFourState);
                    break;

            }
            return;
        }
    }
    private bool IsInAttackRange()
    {
        //if (stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Boss.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Boss.Data.AttackPatternInfoDatas[(int)previousAttackPattern].AttackRange * stateMachine.Boss.Data.AttackPatternInfoDatas[(int)previousAttackPattern].AttackRange;
        
    }

    protected BossAttackPattern GetRandomAttackPattern()
    {
        // 현재 보스의 체력을 가져옴
        float currentHealthPercentage = (float)stateMachine.Boss.BossHealth.health / stateMachine.Boss.BossHealth.maxHealth;

        List<BossAttackPattern> availablePatterns = new List<BossAttackPattern>();


        availablePatterns.Add(BossAttackPattern.Pattern1);
        availablePatterns.Add(BossAttackPattern.Pattern3);


        // 체력이 50% 이하이면 Pattern3을 추가하고 고정으로 실행
        if (currentHealthPercentage <= 0.5f)
        {
            if (!isAttackPattern2)
            {
                isAttackPattern2 = true; 
                return BossAttackPattern.Pattern2;
            }
            availablePatterns.Add(BossAttackPattern.Pattern2);
        }


        // 체력이 30% 이하이면 Pattern4를 추가하고 고정으로 한 번 실행
        if (currentHealthPercentage <= 0.3f)
        {
            if (!isAttackPattern4)
            {
                isAttackPattern4 = true;
                return BossAttackPattern.Pattern4;
            }
            availablePatterns.Add(BossAttackPattern.Pattern4);
        }

        // 이전에 사용한 패턴 제거
        availablePatterns.Remove(previousAttackPattern);

        // 남은 패턴 중에서 랜덤하게 선택
        BossAttackPattern randomPattern = availablePatterns[Random.Range(0, availablePatterns.Count)];

        return randomPattern;
    }
}

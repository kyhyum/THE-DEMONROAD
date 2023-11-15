using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    protected BossAttackPattern previousAttackPattern;

    public BossAttackState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        // 초기화 등 필요한 작업 수행
    }

    public override void Enter()
    {
        GetRandomAttackPattern();
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
            availablePatterns.Add(BossAttackPattern.Pattern2);
        }


        // 체력이 30% 이하이면 Pattern4를 추가하고 고정으로 한 번 실행
        if (currentHealthPercentage <= 0.3f)
        {
            availablePatterns.Add(BossAttackPattern.Pattern4);
        }

        // 이전에 사용한 패턴 제거
        availablePatterns.Remove(previousAttackPattern);

        // 남은 패턴 중에서 랜덤하게 선택
        BossAttackPattern randomPattern = availablePatterns[Random.Range(0, availablePatterns.Count)];

        return randomPattern;
    }
}

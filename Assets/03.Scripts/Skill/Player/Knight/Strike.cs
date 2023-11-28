using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Skill, IUsable
{
    [field:SerializeField] Player player;
    Collider skillCollider;

    int totalDamage;

    private void Awake()
    {
        skillCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        Use();
    }

    public void Use()
    {
        //player.IsAttackSkill1 = true;
        StartCoroutine(strike());
        totalDamage = damage + level * increasedDamagePerLevel;
    }

    IEnumerator strike()
    {
        skillCollider.enabled = true;

        yield return new WaitForSeconds(1f);

        skillCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Strike 클래스 OnTriggerEnter 함수 호출한다.");

        if(player.IsAttackSkill1)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Strike 클래스 OnTriggerEnter 함수. Enemy 이다.");

                if (other.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    Debug.Log($"데미지 {totalDamage}를 입혔다.");

                    enemyHealth.TakeDamage(totalDamage);
                }
            }
        }

        
    }
}
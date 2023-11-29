using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange1 : MonoBehaviour
{
    [field: SerializeField] public Player player;
    [field: SerializeField] public PlayerStateMachine stateMachine;
    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //GameManager.Instance._player

        Debug.Log("AttackRange1 클래스 OnTriggerEnter 함수 호출한다.");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("AttackRange1 클래스 OnTriggerEnter 함수. Enemy 이다.");

            if (other.TryGetComponent(out EnemyHealth enemyHealth))
            {
                //Debug.Log($"데미지 {knightSkill.totalDamage}를 입혔다.");

                //enemyHealth.TakeDamage(knightSkill.totalDamage);
            }
        }
    }
}

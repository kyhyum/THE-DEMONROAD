using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Collider myCollider;

    private int damage;

    // 이미 충돌한 상태
    private List<Collider> alreadyColliderWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyColliderWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;

        if (alreadyColliderWith.Contains(other)) return;

        alreadyColliderWith.Add(other);

        // Component.TryGetComponent(): 지정된 타입의 컴포넌트(있는 경우)를 가져온다.
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}

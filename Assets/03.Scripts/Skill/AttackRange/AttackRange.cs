using UnityEngine;

public class AttackRange : MonoBehaviour
{
    float damage;

    public void Set(float totalDamage)
    {
        damage = totalDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Monster"))
            return;

        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage((int)damage);
        }
    }
}
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private Collider rangeCollider;
    float damage;
    private void Awake()
    {
        rangeCollider = GetComponent<Collider>();
    }

    public void Set(float totalDamage)
    {
        damage = totalDamage;
    }

    public void SetActive(bool flag)
    {
        rangeCollider.enabled = flag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Monster"))
            return;

        // TODO: 몬스터에게 damage만큼 피해주기
    }
}
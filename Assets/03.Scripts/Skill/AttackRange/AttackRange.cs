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

    }
}
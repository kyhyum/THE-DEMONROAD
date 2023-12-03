using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public Collider myCollider { set; get; }

    public int damage;

    public List<Collider> alreadyColliderWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyColliderWith.Clear();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (alreadyColliderWith.Contains(other)) return;

        alreadyColliderWith.Add(other);
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

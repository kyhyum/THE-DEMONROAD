using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossSpell : BossWeapon
{
    public event Action<BossSpell> BulletReturned;
    public float bulletDuration = 11f;
    Coroutine clearCoroutine = null;
    public void BulletSpawn()
    {
        StartCoroutine(BulletUnactive());
        clearCoroutine = StartCoroutine(ClearColliderArray());
    }

    IEnumerator BulletUnactive()
    {
        yield return new WaitForSeconds(bulletDuration);
        
        BulletReturned?.Invoke(this);
        StopCoroutine(clearCoroutine);
    }

    public override void OnTriggerEnter(Collider other)
    {
        //if (other == myCollider) return;
        //if (alreadyColliderWith.Contains(other)) return;

        //alreadyColliderWith.Add(other);
        //if (other.TryGetComponent(out ITakeDamage takeDamage))
        //{
        //    takeDamage.TakeDamage(damage);
        //}
    }

   
    private void OnParticleCollision(GameObject other)
    {
        Collider otherCollider = other.GetComponent<Collider>();
        if (other == myCollider) return;
        if (alreadyColliderWith.Contains(otherCollider)) return;

        alreadyColliderWith.Add(otherCollider);
        if (other.TryGetComponent(out ITakeDamage takeDamage))
        {
            takeDamage.TakeDamage(damage);
        }
        Debug.Log("Particle collided with: " + other.name);
    }

    IEnumerator ClearColliderArray()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            alreadyColliderWith.Clear();
        }
    }

    public void SpellSetEventNull()
    {
        BulletReturned = null;
    }
}

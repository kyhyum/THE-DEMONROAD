using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : BossWeapon
{
    public event Action<BossSpell> BulletReturned;
    public float bulletDuration = 11f;
    public void BulletSpawn()
    {
        StartCoroutine(BulletUnactive());
    }

    IEnumerator BulletUnactive()
    {
        yield return new WaitForSeconds(bulletDuration);

        BulletReturned?.Invoke(this);
    }
    public void SpellSetEventNull()
    {
        BulletReturned = null;
    }
}

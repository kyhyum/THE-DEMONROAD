using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : BossWeapon
{
    public event Action<BossBullet> BulletReturned;
    public float bulletSpeed = 10f;
    public float bulletDuration = 11f;

    public Coroutine DestroyCoroutine = null;

    private Transform Target;
    void Awake()
    {
        Target = GameManager.Instance.Myplayer.transform;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (alreadyColliderWith.Contains(other)) return;

        alreadyColliderWith.Add(other);
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
            BulletReturned?.Invoke(this);
            BulletReturned = null;
        }
    }

    public void Shooting()
    {
        StartCoroutine(FlyTowardsTarget());
    }

    IEnumerator FlyTowardsTarget()
    {
        float elapsedTime = 0f;
        // 플레이어 방향으로 회전
        Target.position += Vector3.up * 1.2f;
        transform.LookAt(Target.position);

        while (elapsedTime < bulletDuration)
        {
            // 총알을 플레이어 방향으로 날아가게 함
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        BulletReturned?.Invoke(this);
    }

    public void BulletSetEventNull()
    {
        BulletReturned = null;
    }

}

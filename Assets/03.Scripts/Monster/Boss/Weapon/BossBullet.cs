using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : BossWeapon
{
    public event Action<BossBullet> BulletReturned;
    public float bulletSpeed = 10f;
    public float bulletDuration = 11f;

    private Transform Target;
    void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Shooting()
    {
        StartCoroutine(FlyTowardsTarget());
    }
    IEnumerator FlyTowardsTarget()
    {
        float elapsedTime = 0f;

        while (elapsedTime < bulletDuration)
        {
            // 플레이어 방향으로 회전
            transform.LookAt(Target.position);

            // 총알을 플레이어 방향으로 날아가게 함
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 일정 시간이 지나면 총알 오브젝트 풀에 넣기
        BulletReturned?.Invoke(this);
    }
}

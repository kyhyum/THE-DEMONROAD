using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpellRotation : MonoBehaviour
{
    private List<Transform> initialBulletTrs;
    public List<Transform> bullets = new List<Transform>();
    Coroutine rotationCoroutine = null;

    private void Awake()
    {
        initialBulletTrs = new List<Transform>(bullets);
    }
    private void OnEnable()
    {
        bullets = initialBulletTrs;
        rotationCoroutine = StartCoroutine(rotateBullet());
    }

    private void OnDisable()
    {
        StopCoroutine(rotationCoroutine);
    }
    IEnumerator rotateBullet()
    {
        float delayTime = 0;
        while (true)
        {
            delayTime += Time.deltaTime;
            this.transform.rotation = Quaternion.EulerAngles(new Vector3(0, delayTime / 1.2f, 0));
            if (delayTime > 5f)
            {
                MoveBullets();
            }
            if(delayTime > 11f)
            {
                yield return null;
            }

        }
    }

    public void MoveBullets()
    {
        for(int i = 0; i < bullets.Count; i++) {
            bullets[i].LookAt(this.transform);
            bullets[i].Translate(Vector3.forward / 150f);
        }
    }

}

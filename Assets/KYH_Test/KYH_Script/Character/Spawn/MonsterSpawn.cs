using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public float height;
    public List<MonsterSpawnInfo> spawnList = new List<MonsterSpawnInfo>();
    public List<BoxCollider> rangeCollider;

    public Vector3 ReturnRandomPosition()
    {
        int idx = Random.Range(0, rangeCollider.Count);
        Vector3 originPosition = rangeCollider[idx].gameObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider[idx].bounds.size.x;
        float range_Z = rangeCollider[idx].bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
}

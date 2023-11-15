using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    List<GameObject>[] pools;
    Dictionary<string, int> objectIndex;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }

        pools = new List<GameObject>[10];
        objectIndex = new Dictionary<string, int>();
    }

    public void Set(string key)
    {
        if (!objectIndex.ContainsKey(key))
        {
            int index = objectIndex.Count;
            pools[index] = new List<GameObject>();
            objectIndex.Add(key, objectIndex.Count);
        }
    }

    public GameObject Get(string key)
    {
        GameObject select = null;

        if (objectIndex.TryGetValue(key, out int index))
        {
            foreach (GameObject item in pools[index])
            {
                if (!item.activeSelf)
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }
            if (!select)
            {
                select = Instantiate(gameObject, transform);
                pools[index].Add(select);
            }
        }

        return select;
    }
}

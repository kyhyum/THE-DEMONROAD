using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    List<GameObject>[] pools;
    Dictionary<GameObject, int> objectIndex;

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
        objectIndex = new Dictionary<GameObject, int>();
    }

    public void Set(GameObject gameObject, int num)
    {
        if (objectIndex.ContainsKey(gameObject))
        {

        }
        else
        {
            int index = objectIndex.Count;
            pools[index] = new List<GameObject>();
            objectIndex.Add(gameObject, objectIndex.Count);

            for (int i = 0; i < num; i++)
            {
                GameObject aaa = Instantiate(gameObject, transform);
                aaa.SetActive(false);
                pools[index].Add(aaa);
            }
        }
    }

    public GameObject Get(GameObject gameObject)
    {
        GameObject select = null;

        if (objectIndex.TryGetValue(gameObject, out int index))
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

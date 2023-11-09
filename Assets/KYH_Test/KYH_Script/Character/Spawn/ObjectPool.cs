using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private List<T> pooledObjects;
    private T prefab;
    private Transform parentTransform;
    private int listSize;

    public ObjectPool(T prefab, int initialSize, Transform parentTransform = null)
    {
        listSize = initialSize;
        this.prefab = prefab;
        this.parentTransform = parentTransform;
        pooledObjects = new List<T>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    public T GetObject()
    {
        if (pooledObjects.Count == 0)
        {
            CreateObject();
            listSize++;
        }

        T obj = pooledObjects[pooledObjects.Count - 1];
        pooledObjects.RemoveAt(pooledObjects.Count - 1);
        obj.gameObject.SetActive(true);

        return obj;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj);
    }

    private T CreateObject()
    {
        T obj = Object.Instantiate(prefab, parentTransform);
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj);

        return obj;
    }

    public bool CheckListSize()
    {
        if(pooledObjects.Count == listSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

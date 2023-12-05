using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<T>();
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}

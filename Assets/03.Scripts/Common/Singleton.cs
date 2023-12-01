using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    string componentName = typeof(T).ToString();

                    GameObject findObject = GameObject.Find(componentName);

                    if (findObject == null)
                    {
                        findObject = new GameObject(componentName);
                    }

                    _instance = findObject.AddComponent<T>();
                }

                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }
}

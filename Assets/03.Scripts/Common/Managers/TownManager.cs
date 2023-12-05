using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.ActivePlayerUI(true);
        GameManager.Instance.condition.GenerateResource();
    }
}

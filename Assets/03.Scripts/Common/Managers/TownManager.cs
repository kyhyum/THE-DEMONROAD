using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    void Start()
    {
        UIManager.Instance.ActivePlayerUI(true);
        GameManager.Instance.condition.GenerateResource();
        SoundManager.Instance.BGMPlay(clip);
    }
}

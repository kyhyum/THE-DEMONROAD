using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterCreate : MonoBehaviour
{
    public GameObject jobCharacter, jobImage;
    [SerializeField] int jobIndex;
    public void ChooseJob()
    {
        jobCharacter.SetActive(true);
        StartSceneManager.s_instance.selectJobIndex = jobIndex;
        StartSceneManager.s_instance.ChangeJob();
    }
}

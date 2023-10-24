using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterCreate : MonoBehaviour
{
    [SerializeField] GameObject jobCharacter;
    [SerializeField] int jobIndex;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChooseJob()
    {
        Instantiate(jobCharacter);
        StartSceneManager.s_instance.ChangeJobImage(jobIndex);
    }
    public void CreateCharacter()
    {
        string nameText = StartSceneManager.s_instance.nameCreate.text;
        string prefabPath = "Assets/Resources/MyCharacter/" + jobCharacter.name + ".prefab";
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(jobCharacter, prefabPath);
        StartSceneManager.s_instance.characterSlot.character = prefab;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    public Vector3 characterPos;
    public GameObject character;
    [SerializeField] GameObject createButton;
    private void Start()
    {
        if (character != null)
        {
            character.transform.SetParent(this.transform);
            character.transform.position = characterPos;
            //character.transform.LookAt(Camera.main.transform.position);
            createButton.SetActive(false);
        }
    }
    public void SelectSlot()
    {
        StartSceneManager.s_instance.characterSlot = this;
    }
    public void CreateButton()
    {
        StartSceneManager.s_instance.characterSlot = this;
        StartSceneManager.s_instance.OpenCreateCanvas();
    }
    
}

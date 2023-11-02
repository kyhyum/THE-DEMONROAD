using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public Vector3 characterPos;
    public GameObject character;
    public int slotIndex;
    [SerializeField] GameObject createButton;
    [SerializeField] TextMeshProUGUI characterName, characterLevel, characterJob;
    private void OnEnable()
    {
        if (character != null)
        {
            PlayerData data = character.GetComponent<PlayerCondition>().playerData;
            character.name = data.name;
            character.transform.SetParent(this.transform);
            character.transform.position = characterPos;
            character.transform.localScale = new Vector3(3, 3, 3);
            character.transform.LookAt(Camera.main.transform.position);
            characterName.text = character.name;
            characterLevel.text = data.level.ToString();
            characterJob.text = data.job.ToString();
            createButton.SetActive(false);
            TextOpen(true);
        }
        else
        {
            TextOpen(false);
        }
    }
    public void SelectSlot()
    {
        StartSceneManager.s_instance.selectedSlot = slotIndex;
    }
    public void CreateButton()
    {
        StartSceneManager.s_instance.selectedSlot = slotIndex;
        StartSceneManager.s_instance.OpenCreateCanvas();
    }
    void TextOpen(bool isChar)
    {
        characterName.gameObject.SetActive(isChar);
        characterLevel.gameObject.SetActive(isChar);
        characterJob.gameObject.SetActive(isChar);
    }
    
}

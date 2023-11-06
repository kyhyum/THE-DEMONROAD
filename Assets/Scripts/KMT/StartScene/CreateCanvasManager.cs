using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateCanvasManager : MonoBehaviour
{
    public static CreateCanvasManager s_instance;
    
    public int selectJobIndex;

    [SerializeField] TMP_InputField nameCreate;
    [SerializeField] CharacterCreate[] job;
    SelectCanvasManager selectCanvasManager;
    PlayerData createCharacterData;
    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this);
        }
        selectCanvasManager = SelectCanvasManager.s_instance;
    }
    private void OnEnable()
    {
        nameCreate.text = null;
        selectJobIndex = 0;
        job[selectJobIndex].jobCharacter.SetActive(true);
    }
    private void OnDisable()
    {
        job[selectJobIndex].jobCharacter.SetActive(false);
    }
    public void CreateCharacter()
    {
        if (!selectCanvasManager.playerName.Contains(nameCreate.text) && selectJobIndex == 0)
        {
            string nameText = nameCreate.text;
            selectCanvasManager.createSlot.character = Instantiate(job[selectJobIndex].jobCharacter);
            createCharacterData = job[selectJobIndex].playerData;
            createCharacterData.name = nameText;
            createCharacterData.playerIndex = selectCanvasManager.createSlot.slotIndex;
            createCharacterData.level = 1;
            GameManager.s_instance.SavePlayerDataToJson(StringManager.jsonPath, createCharacterData.name, createCharacterData);
            selectCanvasManager.createSlot.character.AddComponent<PlayerCondition>().playerData = GameManager.s_instance.LoadPlayerDataFromJson(StringManager.jsonPath, createCharacterData.name);
            selectCanvasManager.playerDatas[createCharacterData.playerIndex] = createCharacterData;
            selectCanvasManager.playerName.Add(nameText);
            StartSceneManager.s_instance.OpenSelectCanvas();
        }
        else if (selectJobIndex != 0)
        {
            Debug.Log("아직 개발 단계라 생성할 수 없습니다");
        }
        else
        {
            Debug.Log("이미 있는 이름입니다");
        }
    }
    public void ChangeJob(int jobIndex)
    {
        for (int i = 0; i < job.Length; i++)
        {
            job[i].jobImage.SetActive(false);
            job[i].jobCharacter.SetActive(false);
            if (i == jobIndex)
            {
                selectJobIndex = i;
                job[i].jobImage.SetActive(true);
                job[i].jobCharacter.SetActive(true);
                job[i].ChoiceJob();
            }
        }
    }
}

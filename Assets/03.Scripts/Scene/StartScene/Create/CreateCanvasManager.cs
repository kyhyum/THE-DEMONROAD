using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateCanvasManager : MonoBehaviour
{
    public static CreateCanvasManager Instance;
    
    int selectJobIndex;

    [SerializeField] TMP_InputField nameCreate;
    [SerializeField] CharacterCreate[] job;
    SelectCanvasManager selectCanvasManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        selectCanvasManager = SelectCanvasManager.Instance;
    }
    private void OnEnable()
    {
        nameCreate.text = null;
        selectJobIndex = 0;
        job[selectJobIndex].ActiveObject(true);
    }
    private void OnDisable()
    {
        job[selectJobIndex].ActiveObject(false);
    }
    public void CreateCharacter()
    {
        if(selectJobIndex == 0)
        {
            selectCanvasManager.CreateCharacter(nameCreate.text, job[selectJobIndex].playerData);
        }
    }
    public void ChangeJob(int jobIndex)
    {
        for (int i = 0; i < job.Length; i++)
        {
            job[i].ActiveObject(false);
            
            if (i == jobIndex)
            {
                selectJobIndex = i;
                job[i].ActiveObject(true);
                job[i].ChoiceJob();
            }
        }
    }
}

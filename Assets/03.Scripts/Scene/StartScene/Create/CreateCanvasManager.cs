using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CreateCanvasManager : MonoBehaviour
{
    public static CreateCanvasManager Instance;
    
    int selectJobIndex;

    [SerializeField] TMP_InputField nameCreate;
    [SerializeField] CharacterCreate[] job;
    [SerializeField] GameObject empty;
    SelectCanvasManager selectCanvasManager;
    [SerializeField] Camera mainCamera;
    Tween tween;
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
        ChangeJob(0);
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
        empty.SetActive(false);
        for (int i = 0; i < job.Length; i++)
        {
            job[i].ActiveObject(false);
            
            if (i == jobIndex)
            {
                selectJobIndex = i;
                job[i].ActiveObject(true);
                job[i].Setting();
                job[i].ChoiceJob();
                ChoiceProduction();
            }
        }
    }
    void ChoiceProduction()
    {
        tween = mainCamera.DOShakePosition(2.4f, 0.2f);
        tween.OnComplete(EmptySetActive);
    }
    void EmptySetActive()
    {
        empty.SetActive(true);
    }
}

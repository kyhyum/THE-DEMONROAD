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
            SelectCanvasManager.Instance.CreateCharacter(nameCreate.text, job[selectJobIndex].playerData);
        }
        else
        {
            UIManager.Instance.ActivePopUpUI("캐릭터 생성", "현재 생성이 불가능한 캐릭입니다.", null);
        }
    }
    public void ChangeJob(int jobIndex)
    {
        empty.SetActive(false);
        nameCreate.text = null;
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

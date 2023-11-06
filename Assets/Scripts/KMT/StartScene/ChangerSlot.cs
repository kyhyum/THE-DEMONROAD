using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangerSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI index, charLevel, charName, charJob;
    [SerializeField] GameObject UpButton, DownButton;
    [SerializeField] private int slotIndex;
    SelectCanvasManager selectCanvasManager;
    private PlayerData playerData;
    public PlayerData PlayerData { get { return playerData; } }
    private void OnEnable()
    {
        if(selectCanvasManager == null)
        {
            selectCanvasManager = SelectCanvasManager.s_instance;
        }
        if(selectCanvasManager.PlayerDatas[slotIndex] != null)
        {
            playerData = selectCanvasManager.PlayerDatas[slotIndex];
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        if(playerData == null)
        {
            UISetActive(false);
        }
        else
        {
            UISetActive(true);
            index.text = (playerData.playerIndex + 1).ToString() + ".";
            charLevel.text = "Lv." + playerData.level.ToString();
            charName.text = playerData.name.ToString();
            charJob.text = playerData.job.ToString();
            if(playerData.playerIndex == 0)
            {
                UpButton.SetActive(false);
            }
            else if(playerData.playerIndex == 3)
            {
                DownButton.SetActive(false);
            }
        }
    }
    void UISetActive(bool isActive)
    {
        index.gameObject.SetActive(isActive);
        charLevel.gameObject.SetActive(isActive);
        charName.gameObject.SetActive(isActive);
        charJob.gameObject.SetActive(isActive);
        UpButton.SetActive(isActive);
        DownButton.SetActive(isActive);
    }
    public void SetData(int index ,PlayerData data)
    {
        playerData = data;
        if (playerData != null)
        {
            playerData.playerIndex = index;
        }
        UpdateUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    List<Resolution> resolutions = new List<Resolution>();
    Dictionary<int, Resolution> resolutionsDic = new Dictionary<int, Resolution>();
    [SerializeField] TMP_Dropdown resolutionDropDown;
    [SerializeField] Toggle fullScreenToggle;
    int resolutionIndex;
    FullScreenMode screenMode;
    void Start()
    {
        InitUI();
    }
    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRateRatio.value == 60f)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        resolutionDropDown.options.Clear();
        int optionIndex = 0;
        foreach(Resolution one in resolutions)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = $"{one.width} × {one.height}";
            resolutionDropDown.options.Add(optionData);
            if (one.width == Screen.width && one.height == Screen.height)
            {
                resolutionDropDown.value = optionIndex;
            }
            optionIndex++;
        }
        resolutionDropDown.RefreshShownValue();

        fullScreenToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }
    public void DropDownOptionChange(int x)
    {
        resolutionIndex = x;
    }
    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    public void OpenPopUp()
    {
        UIManager.Instance.popUpUI.OpenPopUpUI("디스플레이", "환경 설정을 변경 하시겠습니까?", ChangeDisplay);
    }
    void ChangeDisplay()
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, screenMode);
    }
}

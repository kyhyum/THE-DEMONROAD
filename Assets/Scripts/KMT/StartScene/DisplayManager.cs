using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    List<Resolution> resolutions = new List<Resolution>();
    [SerializeField] TMP_Dropdown resolutionDropDown;
    [SerializeField] Toggle fullScreenToggle;
    int resolutionIndex;
    FullScreenMode screenMode;
    void Start()
    {
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitUI()
    {
        /*for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRateRatio.Equals(60))
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }*/
        resolutions.AddRange(Screen.resolutions);
        resolutionDropDown.options.Clear();
        int optionIndex = 0;
        foreach(Resolution one in resolutions)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = $"{one.width} × {one.height}";
            resolutionDropDown.options.Add(optionData);
            if(one.width ==Screen.width && one.height == Screen.height)
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
    public void ChangeDisplay()
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, screenMode);
    }
}

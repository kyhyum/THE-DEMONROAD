using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public Image hp;
    public TMP_Text hpText;
    public Image mp;
    public TMP_Text mpText;
    public Image exp;
    public TMP_Text expText;

    public void UpdateHpUI(float newValue, float maxValue)
    {
        hp.fillAmount = newValue / maxValue;
        hpText.text = string.Format("{0} / {1}", (int)newValue, (int)maxValue);
    }

    public void UpdateMpUI(float newValue, float maxValue)
    {
        mp.fillAmount = newValue / maxValue;
        mpText.text = string.Format("{0} / {1}", (int)newValue, (int)maxValue);

    }

    public void UpdateExpUI(float newValue, float maxValue)
    {
        float persent = newValue / maxValue;
        exp.fillAmount = persent;
        expText.text = string.Format("{0:N0} / {1:N0} ({2:F2}%)", (int)newValue, (int)maxValue, persent * 100);
    }
}

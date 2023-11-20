using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PopUpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title, explan;
    [SerializeField] Button yesButton, noButton, okButton;
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenPopUpUI(string titleText, string explanText, UnityAction action)
    {
        this.gameObject.SetActive(true);

        title.text = titleText;
        explan.text = explanText;

        yesButton.onClick.AddListener(action);

        if(action == null)
        {
            okButton.gameObject.SetActive(true);
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
        }
        else
        {
            okButton.gameObject.SetActive(false);
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);
        }
    }
}

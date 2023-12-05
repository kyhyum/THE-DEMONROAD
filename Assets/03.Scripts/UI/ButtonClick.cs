using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        Debug.Log(SoundManager.Instance);
        button.onClick.AddListener(SoundManager.Instance.ButtonClick);
    }
}

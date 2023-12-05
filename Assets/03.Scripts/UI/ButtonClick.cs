using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(SoundManager.Instance.ButtonClick);
    }
}

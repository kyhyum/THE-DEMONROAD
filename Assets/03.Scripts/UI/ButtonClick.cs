using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    Button button;
    SoundManager soundManager;
    private void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        soundManager = SoundManager.Instance;
        button.onClick.AddListener(soundManager.ButtonClick);
    }
}

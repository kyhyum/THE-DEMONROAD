using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject settingCanvas;
    public static GameManager s_instance;
    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void Finish()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void Save()
    {

    }
    public void Load()
    {

    }
}

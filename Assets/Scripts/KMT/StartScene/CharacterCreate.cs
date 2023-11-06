using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterCreate : MonoBehaviour
{
    public GameObject jobCharacter, jobImage;
    [SerializeField] int jobIndex;
    public PlayerData playerData;
    public void ChooseJob()
    {
        CreateCanvasManager.s_instance.selectJobIndex = jobIndex;
        CreateCanvasManager.s_instance.ChangeJob();
        if (jobCharacter.TryGetComponent<Animator>(out Animator animator))
        {
            animator.SetTrigger("Slash");
        }
    }
}

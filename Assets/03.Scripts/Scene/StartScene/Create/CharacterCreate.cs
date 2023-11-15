using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreate : MonoBehaviour
{
    [SerializeField] GameObject jobCharacter, jobImage;
    public PlayerData playerData;
    public void ChoiceJob()
    {
        if (jobCharacter.TryGetComponent<Animator>(out Animator animator))
        {
            animator.SetTrigger("Slash");
        }
    }
    public void ActiveObject(bool isActive)
    {
        if(jobCharacter == null)
        {
            return;
        }
        jobCharacter.SetActive(isActive);
        jobImage.SetActive(isActive);
    }
}

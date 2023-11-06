using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreate : MonoBehaviour
{
    public GameObject jobCharacter, jobImage;
    public PlayerData playerData;
    public void ChoiceJob()
    {
        if (jobCharacter.TryGetComponent<Animator>(out Animator animator))
        {
            animator.SetTrigger("Slash");
        }
    }
}

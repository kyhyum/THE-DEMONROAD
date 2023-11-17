using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreate : MonoBehaviour
{
    [SerializeField] GameObject jobCharacter, jobImage;
    public PlayerData playerData;
    Animator animator;
    VoiceClip voiceClip;
    AudioSource audioSource;
    public void Setting()
    {
        animator = jobCharacter.GetComponent<Animator>();
        voiceClip = jobCharacter.GetComponent<VoiceClip>();
        audioSource = jobCharacter.GetComponent<AudioSource>();
    }
    public void ChoiceJob()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetTrigger("Slash");
        audioSource.clip = voiceClip.clips[0];
        audioSource.Play();
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

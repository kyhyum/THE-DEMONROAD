using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    public AudioClip monsterAttackClip;
    public AudioClip monsterDeadClip;
   
    private AudioSource monsterAudioSource;

    private void Start()
    {
        monsterAudioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        monsterAudioSource.clip = monsterAttackClip;
        monsterAudioSource.Play();
    }

    public void PlayDeadSound()
    {
        monsterAudioSource.clip = monsterDeadClip;
        monsterAudioSource.Play();
    }
}

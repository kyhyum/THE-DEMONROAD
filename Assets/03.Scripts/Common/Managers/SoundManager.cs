using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource buttonClickSource;
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle muteToggle;
    bool mute;
    float master;
    float bgm;
    float sfx;

    private void Start()
    {
        bgmAudioSource.Play();

        master = PlayerPrefs.GetFloat("Master");
        masterMixer.SetFloat("Master", master);
        masterSlider.value = master;
        bgm = PlayerPrefs.GetFloat("BGM");
        masterMixer.SetFloat("BGM", bgm);
        bgmSlider.value = bgm;
        sfx = PlayerPrefs.GetFloat("SFX");
        masterMixer.SetFloat("SFX", sfx);
        sfxSlider.value = sfx;
        mute = PlayerPrefs.GetInt("Mute") == 1;
        muteToggle.isOn = mute;
        MuteCheck(muteToggle.isOn);
    }
    public void MasterControl()
    {
        master = masterSlider.value;
        PlayerPrefs.SetFloat("Master", master);


        if (mute)
        {
            return;
        }

        if (master == -40f)
        {
            masterMixer.SetFloat("Master", -80);
        }
        else
        {
            masterMixer.SetFloat("Master", master);
        }

    }
    public void BGMControl()
    {
        bgm = bgmSlider.value;
        PlayerPrefs.SetFloat("BGM", bgm);

        if (mute)
        {
            return;
        }

        if (bgm == -40f)
        {
            masterMixer.SetFloat("BGM", -80);
        }
        else
        {
            masterMixer.SetFloat("BGM", bgm);
        }

    }
    public void SFXControl()
    {
        sfx = sfxSlider.value;
        PlayerPrefs.SetFloat("SFX", sfx);

        if (mute)
        {
            return;
        }

        if (sfx == -40f)
        {
            masterMixer.SetFloat("SFX", -80f);
        }
        else
        {
            masterMixer.SetFloat("SFX", sfx);
        }

    }
    public void MuteCheck(bool isMute)
    {
        mute = isMute;
        masterMixer.SetFloat("BGM", mute ? -80f : bgm);
        masterMixer.SetFloat("SFX", mute ? -80f : sfx);
        PlayerPrefs.SetInt("Mute", mute ? 1 : 0);
    }
    public void BGMPlay(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }
    public void SFXPlay(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    public void ButtonClick()
    {
        buttonClickSource.Play();
    }
}
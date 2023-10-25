using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.Timeline;

public class SoundManager : MonoBehaviour
{
    public static SoundManager s_instance;
    public AudioSource[] audioSources;
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle muteToggle;
    float master;
    float bgm;
    float sfx;
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
    private void Start()
    {
        master = PlayerPrefs.GetFloat("Master");
        masterMixer.SetFloat("Master", master);
        masterSlider.value = master;
        bgm = PlayerPrefs.GetFloat("BGM");
        masterMixer.SetFloat("BGM", bgm);
        bgmSlider.value = bgm;
        sfx = PlayerPrefs.GetFloat("SFX");
        masterMixer.SetFloat("SFX", sfx);
        sfxSlider.value = sfx;
        muteToggle.isOn = PlayerPrefs.GetInt("Mute") == 1;
        MuteCheck(muteToggle.isOn);
    }
    public void MasterControl()
    {
        master = masterSlider.value;
        if (master == -40f)
        {
            masterMixer.SetFloat("Master", -80);
        }
        else 
        {
            masterMixer.SetFloat("Master", master);
        }
        PlayerPrefs.SetFloat("Master", master);
    }
    public void BGMControl()
    {
        bgm = bgmSlider.value;
        if (bgm == -40f)
        {
            masterMixer.SetFloat("BGM", -80);
        }
        else 
        {
            masterMixer.SetFloat("BGM", bgm);
        }
        PlayerPrefs.SetFloat("BGM", bgm);
    }
    public void SFXControl()
    {
        sfx = sfxSlider.value;
        if (sfx == -40f)
        {
            masterMixer.SetFloat("SFX", -80f);
        }
        else 
        {
            masterMixer.SetFloat("SFX", sfx);
        }
        PlayerPrefs.SetFloat("SFX", sfx);
    }
    public void MuteCheck(bool isMute)
    {
        masterMixer.SetFloat("BGM", isMute? -80f : bgm);
        masterMixer.SetFloat("SFX", isMute? -80f : sfx);
        PlayerPrefs.SetInt("Mute", isMute ? 1 : 0);
    }
}
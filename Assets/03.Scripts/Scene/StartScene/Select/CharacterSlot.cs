using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] Vector3 characterPos;
    [SerializeField] Quaternion look;
    [SerializeField] GameObject createButton;
    [SerializeField] TextMeshProUGUI characterName, characterLevel, characterJob;
    [SerializeField] GameObject character;

    PlayerCondition conditon;
    public PlayerData data;
    VoiceClip clip;
    AudioSource source;
    Animator animator;
    private void OnEnable()
    {
        SlotSetting();
    }
    public void SlotSetting()
    {
        if (character != null)
        {
            character.SetActive(true);
            Setting();
            conditon = character.GetComponent<PlayerCondition>();
            data = conditon.playerData;
            TextUpdate(data);
        }
        else
        {
            TextOpen(false);
        }
    }
    private void Setting()
    {
        animator = character.GetComponent<Animator>();
        clip = character.GetComponent<VoiceClip>();
        source = character.GetComponent<AudioSource>();
    }
    public void CreateCharacter(GameObject obj, PlayerData data)
    {
        if (character == null)
        {
            character = Instantiate(obj);
            character.SetActive(true);
            conditon = character.AddComponent<PlayerCondition>();
            conditon.playerData = data;
            Setting();
            conditon.Initialize();
        }
    }
    public void SetActiceCharacter()
    {
        if (character == null)
        {
            return;
        }
        character.SetActive(false);
    }
    public void DeleteCharacter()
    {
        if (character != null)
        {
            if (GameManager.Instance.DeleteCharacter(StringManager.JsonPath, data.name))
            {
                SelectCanvasManager.Instance.DeleteCharacter(data);
                ClearSlot();
            }
            else
            {
                Debug.Log("실패했습니다");
            }
        }
        else
        {
            UIManager.Instance.ActivePopUpUI("캐릭터 삭제", "캐릭터를 선택해 주세요", null);
        }
    }
    public void ClearSlot()
    {
        if(character == null)
        {
            return;
        }
        Destroy(character);
        TextOpen(false);
        data = null;
        character = null;
    }
    public void ChoiceSlot()
    {
        if (character == null)
        {
            return;
        }
        animator.SetTrigger("Choice");
        SoundManager.Instance.SFXPlay(source, clip.clips[1]);
    }
    void TextOpen(bool isChar)
    {
        characterName.gameObject.SetActive(isChar);
        characterLevel.gameObject.SetActive(isChar);
        characterJob.gameObject.SetActive(isChar);
        createButton.SetActive(!isChar);
    }
    void TextUpdate(PlayerData data)
    {
        character.name = data.name;
        character.transform.position = characterPos;
        character.transform.localScale = new Vector3(3, 3, 3);
        character.transform.rotation = look;
        characterName.text = character.name;
        characterLevel.text = "Lv. " + data.level.ToString();
        characterJob.text = data.job.ToString();
        TextOpen(true);
    }
    public void ChangeSlot(GameObject obj, PlayerData data)
    {
        if (character != null)
        {
            ClearSlot();
        }
        CreateCharacter(obj, data);
        TextUpdate(data);
    }
    public void StartCharacter()
    {
        if (character != null)
        {
            GameManager.Instance.data = data;
            GameManager.Instance.GameStart();
        }
        else
        {
            UIManager.Instance.ActivePopUpUI("게임 시작", "캐릭터를 선택해 주세요.", null);
        }
    }
}

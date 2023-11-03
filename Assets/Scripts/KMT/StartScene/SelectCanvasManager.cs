using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SelectCanvasManager : MonoBehaviour
{
    public static SelectCanvasManager s_instance;
    public int selectedSlot;

    public CharacterSlot createSlot;
    [SerializeField] CharacterSlot[] characterSlots;
    public List<string> playerName = new List<string>();
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
        TextAsset[] jsons = Resources.LoadAll<TextAsset>("MyCharacter/");
        if (jsons.Length > 0)
        {
            for (int i = 0; i < jsons.Length; i++)
            {
                playerName.Add(jsons[i].name);
            }
            if (playerName.Count > 0)
            {
                foreach (string one in playerName)
                {
                    PlayerData data = GameManager.s_instance.LoadPlayerDataFromJson(StringManager.jsonPath, one);
                    characterSlots[data.playerIndex].character = Instantiate(StartSceneManager.s_instance.baseCharacters[(int)data.job], characterSlots[data.playerIndex].transform);
                    characterSlots[data.playerIndex].character.SetActive(true);
                    characterSlots[data.playerIndex].character.AddComponent<PlayerCondition>().playerData = data;
                }
            }
        }
        
    }
    private void OnEnable()
    {
        selectedSlot = -1;
    }
    public void StartButon()
    {
        if (selectedSlot == -1)
        {
            Debug.Log("캐릭터를 선택해주세요");
            return;
        }
        if (characterSlots[selectedSlot].character != null)
        {
            GameManager.s_instance.player = characterSlots[selectedSlot].character.GetComponent<PlayerCondition>().playerData;
            SceneManager.LoadScene((int)GameManager.s_instance.player.scene);
            DontDestroyOnLoad(GameManager.s_instance.gameObject);
        }
        else
        {
            Debug.Log("캐릭터 선택해줘");
        }
    }
    public void DeleteButton()
    {
        if (selectedSlot == -1)
        {
            Debug.Log("캐릭터를 선택해주세요");
            return;
        }
        if (characterSlots[selectedSlot].character != null)
        {
            characterSlots[selectedSlot].DeleteCharacter();
        }
        else
        {
            Debug.Log("캐릭터 선택해줘");
        }
    }
}

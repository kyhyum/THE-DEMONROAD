using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class KeySettings : MonoBehaviour
{
    public InputActionAsset inputActionAsset;
    [SerializeField] InputActionReference[] bindings;
    [SerializeField] TextMeshProUGUI[] keyValues; 
    InputActionMap actionMap;
    bool isChangeKey;
    string str;
    int key = -1; 
    //public TMP_InputField inputField;
    string strr;
    private void Start()
    {
        //inputField.onEndEdit.AddListener(ChangeBinding);
        // InputActionMap을 얻어옵니다.
        actionMap = inputActionAsset.actionMaps[0]; // 여기에서는 첫 번째 액션 맵을 사용합니다.
        for (int i = 0; i < keyValues.Length; i++)
        {
            str = actionMap.actions[i].bindings[0].effectivePath;
            keyValues[i].text = str.Substring(str.IndexOf('/') + 1);
        }
    }
    private void OnGUI()
    {
        Event currentEvent = Event.current;
        if (isChangeKey && currentEvent.isMouse)
        {
            if(currentEvent.button == 0)
            {
                bindings[key].action.ApplyBindingOverride(0, "<Mouse>/LeftButton");
            }
            else if(currentEvent.button == 1)
            {
                bindings[key].action.ApplyBindingOverride(0, "<Mouse>/RightButton");
            }
            keyValues[key].text = bindings[key].action.bindings[0].effectivePath.Substring(bindings[key].action.bindings[0].effectivePath.IndexOf('/') + 1);
            isChangeKey = false;
            key = -1;
        }
        else if(isChangeKey && currentEvent.isKey)
        {
            bindings[key].action.ApplyBindingOverride(0, currentEvent.keyCode.ToString());
            keyValues[key].text = bindings[key].action.bindings[0].effectivePath;
            isChangeKey = false;
            key = -1;
        }

    }
    private static void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }
    public void SaveKey()
    {
        inputActionAsset.SaveBindingOverridesAsJson();
        Debug.Log(bindings[3].action.bindings[0].effectivePath);
    }
    public void ChangeKey(int num)
    {
        isChangeKey = true;
        key = num;
    }
    
}

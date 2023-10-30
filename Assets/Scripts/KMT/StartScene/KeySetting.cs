using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
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
    private void Start()
    {
        // InputActionMap을 얻어옵니다.
        actionMap = inputActionAsset.actionMaps[0]; // 여기에서는 첫 번째 액션 맵을 사용합니다.
        /*// 각 액션에 대한 정보를 출력합니다.
        foreach (var action in actionMap)
        {
            Debug.Log("Action Name: " + action.name);
            Debug.Log("Bindings: " + string.Join(", ", action.bindings.Select(b => b.effectivePath)));
            //action.ApplyBindingOverride("<Mouse>/RightButton");
        }*/
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
    public void ChangeKey(int num)
    {
        isChangeKey = true;
        key = num;
    }
}

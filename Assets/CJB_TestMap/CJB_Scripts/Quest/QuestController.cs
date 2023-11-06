using UnityEngine;

public class QuestController : MonoBehaviour
{
    public GameObject questLogPanel;
    public GameObject questProgressPanel;
    private bool isLogVisible = false;

    private void Start()
    {
        questLogPanel.SetActive(false);
        questProgressPanel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            isLogVisible = !isLogVisible;
            questLogPanel.SetActive(isLogVisible);
        }   
    }
    public void OpenProgressUI()
    {
        questLogPanel.SetActive(false);
        questProgressPanel.SetActive(true);
    }
}

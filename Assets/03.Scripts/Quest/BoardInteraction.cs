using UnityEngine;

public class BoardInteraction : MonoBehaviour
{
    
    public GameObject questUI;
    public GameObject interactionPop;
    public GameObject acceptPopup;
    public GameObject cancelPopup;
    

    private bool isUIVisible = false;
    Transform player;

    public float activationDistance = 5f;

    void Start()
    {
        player = GameManager.Instance.Myplayer.transform;
        questUI.SetActive(false);
        
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        
        if (distance <= activationDistance)
        {
            
            interactionPop.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                
                isUIVisible = !isUIVisible;            
                questUI.SetActive(isUIVisible);
            }
        }
        else
        {
            
            questUI.SetActive(false);
            interactionPop.SetActive(false);
            
        }

        if(distance >= activationDistance)
        {
            acceptPopup.SetActive(false);
            cancelPopup.SetActive(false);
        }
    }
}

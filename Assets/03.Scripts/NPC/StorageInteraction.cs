using UnityEngine;

public class StorageInteraction : MonoBehaviour
{
    Transform player;
    public GameObject interactionPopup;   
    private bool isUIVisible = false;

    
    private float activationDistance = 3f;
    void Start()
    {
        player = GameManager.Instance.Myplayer.transform;
        
    }


    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= activationDistance)
        {
            interactionPopup.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                isUIVisible = !isUIVisible;               
                
                UIManager.Instance.ActiveStorage();
            }
        }
        else
        {
            
            interactionPopup.SetActive(false);
        }
    }
}


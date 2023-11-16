using UnityEngine;

public class StorageInteraction : MonoBehaviour
{
    Transform player;
    public GameObject interactionPopup;
    public GameObject storageUI;
    private bool isUIVisible = false;

    public float activationDistance = 5f;
    void Start()
    {
        player = GameManager.Instance.Myplayer.transform;
        storageUI.SetActive(false);
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
                storageUI.SetActive(true);
                
            }
        }
        else
        {
            storageUI.SetActive(false);
            interactionPopup.SetActive(false);
        }
    }
}


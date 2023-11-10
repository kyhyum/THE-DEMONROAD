using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInteraction : MonoBehaviour
{
    
    public GameObject questUI;

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
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                isUIVisible = !isUIVisible;

                
                questUI.SetActive(isUIVisible);
            }
        }
        else
        {
            
            questUI.SetActive(false);
        }
    }
}

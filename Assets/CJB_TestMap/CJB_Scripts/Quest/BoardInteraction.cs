using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInteraction : MonoBehaviour
{
    public GameObject questUI;
    private bool isUIVisible = false;
    public Transform player;

    public float activationDistance = 5f;

    void Start()
    {
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

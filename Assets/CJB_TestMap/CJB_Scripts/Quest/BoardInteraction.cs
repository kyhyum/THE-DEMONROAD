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

        // 거리가 활성화 거리보다 작을 때만 F 키 입력을 감지합니다.
        if (distance <= activationDistance)
        {
            // F 키 입력을 감지합니다.
            if (Input.GetKeyDown(KeyCode.F))
            {
                // UI의 가시성을 변경합니다.
                isUIVisible = !isUIVisible;

                // UI의 가시성에 따라 활성화 또는 비활성화합니다.
                questUI.SetActive(isUIVisible);
            }
        }
        else
        {
            // 거리가 활성화 거리를 벗어난 경우 UI를 비활성화합니다.
            questUI.SetActive(false);
        }
    }
}

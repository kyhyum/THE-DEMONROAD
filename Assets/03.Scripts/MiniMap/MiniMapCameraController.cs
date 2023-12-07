using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraController : MonoBehaviour
{
    Transform playerPos;
    private void Start()
    {
        playerPos = GameManager.Instance.player.gameObject.transform;
    }

    private void LateUpdate()
    {
        gameObject.transform.position = playerPos.position + Vector3.up * 10f;
    }
}

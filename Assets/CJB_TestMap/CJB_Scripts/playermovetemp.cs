using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovetemp : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float turnSpeed = 20.0f;

    private Vector3 lastMousePosition;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // 마우스의 움직임에 따라 플레이어를 회전시킵니다.
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseDelta = currentMousePosition - lastMousePosition;

        transform.Rotate(Vector3.up, mouseDelta.x * turnSpeed * Time.deltaTime);

        lastMousePosition = currentMousePosition;
    }
}
    


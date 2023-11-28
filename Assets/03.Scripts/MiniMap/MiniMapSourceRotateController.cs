using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapSourceRotateController : MonoBehaviour
{
    Transform playerTrs;
    void Start()
    {
        playerTrs = GameManager.Instance.Myplayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = playerTrs.rotation;
        gameObject.transform.Rotate(new Vector3(90, 0, 0));
    }
}

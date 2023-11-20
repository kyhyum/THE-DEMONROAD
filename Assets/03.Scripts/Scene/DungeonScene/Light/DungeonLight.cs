using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLight : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerTrs;
    void Start()
    {
        //playerTrs = GameManager.Instance.Myplayer.transform;
        playerTrs = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        this.gameObject.transform.position = playerTrs.position + Vector3.up * 10f;
    }
}

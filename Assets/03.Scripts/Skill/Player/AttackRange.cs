using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PlayerAttackRange 클래스 OnTriggerEnter 함수 호출한다.");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("적을 공격했다.");
        }
    }
}

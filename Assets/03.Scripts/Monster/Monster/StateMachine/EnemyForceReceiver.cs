using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForceReceiver : MonoBehaviour
{
    [SerializeField] private float drag = 0.3f;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    void Update()
    {
        verticalVelocity = Physics.gravity.y * Time.deltaTime;
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag); 
    }

    public void Reset()
    {
        impact = Vector3.zero;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
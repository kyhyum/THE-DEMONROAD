using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInput2 : MonoBehaviour
{
    private CharacterController characterController;
    private NavMeshAgent agent;
    private Camera camera;
    Animator animator;

    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;
    private string walkAnim = "Walk";

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;

        camera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        //animator.SetBool("Idle", true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, groundLayerMask))
            {
                agent.SetDestination(hit.point);
            }

            // remainingDistance: 현재 경로에서 에이전트의 위치와 목적지 사이의 거리이다. 목적지까지 남은 거리
            // stoppingDistance: 에이전트는 목표 위치에 가까워지면 중지된다.
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                Debug.Log($"1 remainingDistance:{agent.remainingDistance}");

                Debug.Log("플레이어가 움직인다.");

                characterController.Move(agent.velocity * Time.deltaTime);
                //animator.SetBool("Idle", false);
                animator.SetBool(walkAnim, true);

            }
            else
            {
                Debug.Log($"2 remainingDistance:{agent.remainingDistance}");

                Debug.Log("플레이어가 멈춘다.");

                characterController.Move(Vector3.zero);
                animator.SetBool(walkAnim, false);
                //animator.SetBool("Idle", true);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = agent.nextPosition;
    }
}

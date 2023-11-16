using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Open!!");
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        Quaternion leftTargetRotation = Quaternion.Euler(0, -90, 0);
        StartCoroutine(RotateOverTime(leftDoor.transform, leftTargetRotation, 1f));

        Quaternion rightTargetRotation = Quaternion.Euler(0, 90, 0);
        StartCoroutine(RotateOverTime(rightDoor.transform, rightTargetRotation, 1f));
    }

    IEnumerator RotateOverTime(Transform doorTransform, Quaternion targetRotation, float duration)
    {
        float elapsedTime = 0f;
        Quaternion startingRotation = doorTransform.rotation;

        while (elapsedTime < duration)
        {
            Debug.Log(elapsedTime);
            doorTransform.rotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorTransform.rotation = targetRotation;
    }
}
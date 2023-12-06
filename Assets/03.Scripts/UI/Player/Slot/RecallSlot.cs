using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RecallSlot : MonoBehaviour
{
    [field: SerializeField] private InputActionReference inputActionReference;
    [field: SerializeField] private Image cooltimeImg;
    [field: SerializeField] private GameObject effect;
    private float coolTime;
    private float fillAmount;
    private void FixedUpdate()
    {
        if (fillAmount < 0)
            fillAmount = 0;

        if (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime;
            cooltimeImg.fillAmount = fillAmount / coolTime;
        }
    }

    private void OnEnable()
    {
        Enable();
    }

    private void OnDisable()
    {
        Disable();
    }

    private void Recall(InputAction.CallbackContext context)
    {
        if (fillAmount != 0)
            return;

        StartCoroutine(CRecall());
    }

    IEnumerator CRecall()
    {
        GameManager.Instance.player.Agent.SetDestination(GameManager.Instance.player.transform.position);
        GameManager.Instance.player.Agent.velocity = Vector3.zero;
        GameObject obj = Instantiate(effect);
        obj.transform.position = GameManager.Instance.Myplayer.transform.position;
        GameManager.Instance.player.isRecall = true;

        yield return new WaitForSecondsRealtime(3f);

        GameManager.Instance.data.currentPlayerPos = new Vector3(0f, 0f, 0f);
        SceneLoadManager.LoadScene("NewTownScene");
        SetCooltime();
        GameManager.Instance.player.isRecall = false;
    }

    private void SetCooltime()
    {
        coolTime = 60;
        fillAmount = 60;
    }

    public void Enable()
    {
        inputActionReference.action.Enable();
        inputActionReference.action.started += Recall;
    }

    public void Disable()
    {
        inputActionReference.action.Disable();
        inputActionReference.action.started -= Recall;
    }
}

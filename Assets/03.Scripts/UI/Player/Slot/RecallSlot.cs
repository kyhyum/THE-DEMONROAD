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
        inputActionReference.action.Enable();
        inputActionReference.action.started += Recall;
    }

    private void OnDisable()
    {
        inputActionReference.action.Disable();
        inputActionReference.action.started -= Recall;
    }

    private void Recall(InputAction.CallbackContext context)
    {
        if (fillAmount != 0)
            return;

        StartCoroutine(CRecall());
    }

    IEnumerator CRecall()
    {
        GameObject obj = Instantiate(effect);
        obj.transform.position = GameManager.Instance.Myplayer.transform.position;

        yield return new WaitForSecondsRealtime(3f);

        Destroy(obj);
        GameManager.Instance.data.currentPlayerPos = new Vector3(0f, 0f, 0f);
        SceneLoadManager.LoadScene("NewTownScene");
        SetCooltime();
    }

    private void SetCooltime()
    {
        coolTime = 60;
        fillAmount = 60;
    }
}

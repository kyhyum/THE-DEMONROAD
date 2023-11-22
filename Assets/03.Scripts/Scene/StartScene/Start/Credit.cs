using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Credit : MonoBehaviour
{
    [SerializeField] GameObject rootObject;
    [SerializeField] ScrollRect scrollVeiw;
    RectTransform content;
    Vector3 pos;
    Vector2 lastPos;
    float duration = 30;
    Tween tween;
    private void Awake()
    {
        content = scrollVeiw.content;
        pos = content.position;
        lastPos = new Vector2(0, pos.y * -1);
    }
    private void OnEnable()
    {
        content.position = pos;
        tween = content.DOAnchorPos(lastPos, duration);
    }
    private void Start()
    {
        tween.OnComplete(SetActive);
    }
    private void SetActive()
    {
        rootObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    public Material TransparentMaterial;

    private Renderer meshRenderer;
    private Material originalMaterial;

    float elapsedTime = 0f;
    private bool isTransparent = false;


    private Coroutine setTransparentCoroutine; // Added Coroutine reference

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    public void BecomeTransparency()
    {
        if(TransparentMaterial == null)
        {
            // 다른 것
        }
        else
        {
            SetTransparent();
        }
    }

    public void SetTransparent()
    {
        setTransparentCoroutine = StartCoroutine(SetUnTransparent());
        if (isTransparent)
        {
            StopCoroutine(setTransparentCoroutine); // Stop the previous coroutine
            return;
        }
        float lerpValue;
        while (elapsedTime <= 1.0f)
        {
            lerpValue = Mathf.Lerp(0f, 1f, elapsedTime / 1.0f);
            meshRenderer.material.Lerp(originalMaterial, TransparentMaterial, lerpValue);
            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;
        }
        meshRenderer.material = TransparentMaterial;
        isTransparent = true;
    }

    IEnumerator SetUnTransparent()
    {
        yield return new WaitForSeconds(1f);
        float lerpValue;
        while (elapsedTime <= 1.0f)
        {
            lerpValue = Mathf.Lerp(0f, 1f, elapsedTime / 1.0f);
            meshRenderer.material.Lerp(TransparentMaterial, originalMaterial, lerpValue);
            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;
        }
        meshRenderer.material = originalMaterial;
        isTransparent = false;
    }
}
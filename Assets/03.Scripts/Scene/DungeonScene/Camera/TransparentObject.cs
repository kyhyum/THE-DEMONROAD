using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class TransparentObject : MonoBehaviour
{
    private Renderer meshRenderer;

    //타운맵
    public List<Material> TransparentMaterials;
    private Material[] originalMaterials;

    //던전맵
    public Material TransparentMaterial;
    private Material originalMaterial;

    /*------------*/
    float elapsedTime = 0f;
    private bool isTransparent = false;


    private Coroutine setTransparentCoroutine; // Added Coroutine reference

    void Start()
    {
        
        meshRenderer = GetComponent<MeshRenderer>();
        if (TransparentMaterial == null)
        {
            originalMaterials = meshRenderer.sharedMaterials;
        }
        else
        {
            originalMaterial = meshRenderer.material;
        }
    }

    public void BecomeTransparency()
    {
        if(TransparentMaterial == null)
        {
            SetTransparent();
        }
        else
        {
            SetTransparentMaterial();
        }
    }

    //타운맵에서 사용//
    public void SetTransparent()
    {
        //float lerpValue;
        setTransparentCoroutine = StartCoroutine(SetUnTransparent());
        if (isTransparent)
        {
            StopCoroutine(setTransparentCoroutine); // Stop the previous coroutine
            return;
        }

        //while (elapsedTime <= 1.0f)
        //{
        //    lerpValue = Mathf.Lerp(1f, 0f, elapsedTime / 1.0f);

        //    for (int i = 0; i < originalMaterials.Length; i++)
        //    {
        //        meshRenderer.sharedMaterials[i = new Color(1, 1, 1, lerpValue);
        //    }
        //    // 경과 시간 업데이트
        //    elapsedTime += Time.deltaTime;
        //}

        //for (int i = 0; i < originalMaterials.Length; i++)
        //{
        //    meshRenderer.materials[i] = TransparentMaterials[i];
        //}

        meshRenderer.sharedMaterials = TransparentMaterials.ToArray();
        isTransparent = true;
    }

    IEnumerator SetUnTransparent()
    {
        yield return new WaitForSeconds(1f);
        //float lerpValue;
        //while (elapsedTime <= 1.0f)
        //{
        //    lerpValue = Mathf.Lerp(0f, 1f, elapsedTime / 1.0f);
        //    for (int i = 0; i < originalMaterials.Length; i++)
        //    {
        //        meshRenderer.materials[i].Lerp(TransparentMaterials[i], originalMaterials[i], lerpValue);
        //    }
        //    // 경과 시간 업데이트
        //    elapsedTime += Time.deltaTime;
        //}
        //for (int i = 0; i < originalMaterials.Length; i++)
        //{
        //    meshRenderer.materials[i] = originalMaterials[i];
        //}

        meshRenderer.sharedMaterials = originalMaterials;

        isTransparent = false;
    }




    // 던전에서 사용//
    public void SetTransparentMaterial()
    {
        setTransparentCoroutine = StartCoroutine(SetUnTransparentMaterial());
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

    IEnumerator SetUnTransparentMaterial()
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
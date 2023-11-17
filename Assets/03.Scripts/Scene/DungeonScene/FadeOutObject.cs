using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutObject : MonoBehaviour
{
    public float fadeSpeed = 2f; // 사라지는 속도

    private Renderer renderer;
    private Color initialColor = new Color(0.8f,0.8f,0.8f,1.0f);
    private float alpha = 1.0f;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void FadeOut()
    {
        StartCoroutine(OnFadeOut());
    }

    
    private IEnumerator OnFadeOut()
    {
        while (alpha > 0)
        {
            alpha -= fadeSpeed * Time.deltaTime; // 알파 퓨시 서서히 감소
            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            renderer.material.SetColor("Fog Color", newColor);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}

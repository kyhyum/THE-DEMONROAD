using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutObject : MonoBehaviour
{
    private GameObject fadeoutObject; 
    
    public float fadeSpeed = 0.5f; // 사라지는 속도

    private Renderer renderer;
    private Color initialColor;
    private float alpha = 1.0f; 
    public FadeOutObject(GameObject gameObject)
    {
        fadeoutObject = gameObject; 
        renderer = fadeoutObject.GetComponent<Renderer>();
        initialColor = renderer.material.color;
    }

    public void OnEnable()
    {
        StartCoroutine(FadeOut());
    }

    
    private IEnumerator FadeOut()
    {
        while (alpha > 0)
        {
            alpha -= fadeSpeed * Time.deltaTime; // 알파 퓨시 서서히 감소
            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            renderer.material.color = newColor;
            yield return null;
        }

        // 오브젝트가 완전히 사라졌을 때 원하는 동작을 수행
        gameObject.SetActive(false);
    }
}

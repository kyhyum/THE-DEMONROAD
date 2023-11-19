using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    //싱글톤으로 처리해야 할듯
    private static EnemyHealthUI instance = null;

    [SerializeField] private GameObject healthGameObject;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI enemyName; 
    
    private Coroutine setActiveCoroutine; // Added Coroutine reference

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        healthGameObject.SetActive(false);
    }
    public static EnemyHealthUI Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void InitSlider(int maxHealth, int health, string name)
    {
        if (setActiveCoroutine != null || health == 0)
        {
            StopCoroutine(setActiveCoroutine); // Stop the previous coroutine
        }
        healthGameObject.SetActive(true);
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        enemyName.text = name;
        setActiveCoroutine = StartCoroutine(SetUnActiveSldier());
    }

    IEnumerator SetUnActiveSldier()
    {
        yield return new WaitForSeconds(3f); // 3초 지연
        healthGameObject.SetActive(false);
    }
}

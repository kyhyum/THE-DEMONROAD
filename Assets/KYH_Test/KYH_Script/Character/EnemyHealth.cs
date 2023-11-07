using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string enemyName;
    public int maxHealth = 100;
    public int health;
    public event Action OnDie;

    public void InitEnemyHealth(int health, string enemyName)
    {
        this.enemyName= enemyName;
        this.health = health;
        this.maxHealth = health;
    }

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        EnemyHealthUI.Instance.InitSlider(maxHealth, health, enemyName);
        health = Mathf.Max(health - damage, 0);

        if (health == 0)
            OnDie?.Invoke();
    }
}

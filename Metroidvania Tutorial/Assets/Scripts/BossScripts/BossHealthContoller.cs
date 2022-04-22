using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthContoller : MonoBehaviour
{
    public static BossHealthContoller instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider bossHealthSlider;

    public int currentHealth = 30;

    public BossBattle bossBattle;

    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            
            bossBattle.EndBattle();
        }

        bossHealthSlider.value = currentHealth;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    //[HideInInspector]
    public int currentHealth;
    public int maxHealth;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //set health to full on start
        currentHealth = maxHealth;
    }

    //deals damage to player
    public void DamagePlayer(int damageAmount)
    {
        currentHealth -= damageAmount;

        //if health reaches 0 or below, the player dies
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public EnemyData demonSO;

    public bool isInvulnerable = false;
    
    private int currentHealth;

    public GameObject deathEffect;

    private void Start()
    {
        //set enemy instance's current health to the health total in SO
        currentHealth = demonSO.healthTotal;
    }

    private void Update()
    {
        //isInvulnerable = demonSO.pillarActive;
    }

    //function to deal damage to health amount and death effect if health reaches 0
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }
            
            Destroy(gameObject);
        }
    }
}

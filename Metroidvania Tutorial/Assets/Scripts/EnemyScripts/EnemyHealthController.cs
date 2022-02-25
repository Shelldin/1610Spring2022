using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int totalHealth = 3;

    public GameObject deathEffect;

    public void DamageEnemy(int damageAmount)
    {
        //subtract health from enemy
        totalHealth -= damageAmount;
    
        //destroy enemy if health drops to 0 or below
        if (totalHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }
            
            Destroy(gameObject);
        }
    }
}

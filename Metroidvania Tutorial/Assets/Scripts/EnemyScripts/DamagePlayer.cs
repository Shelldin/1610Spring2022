using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageAmount = 1;

    public bool destroyOnDamage;
    public GameObject destroyEffect;

    //Calls DealDamage for collisions
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    //Calls DealDamage for triggers
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    //deals damage when called by other functions
    private void DealDamage()
    {
        PlayerHealthController.instance.DamagePlayer(damageAmount);
        
        //if destoryOnDamage is true, enemy will destroy itself when it deals damage with an effect
        if (destroyOnDamage)
        {
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}

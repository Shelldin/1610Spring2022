using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageAmount = 1;

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
    }
}

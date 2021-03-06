using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public GameObject pickupEffect;

    //heals the player when player gets the health pickup
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerHealthController.instance.HealPlayer(healAmount);

            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }
}

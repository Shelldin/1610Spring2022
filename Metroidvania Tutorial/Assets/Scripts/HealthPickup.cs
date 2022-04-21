using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
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

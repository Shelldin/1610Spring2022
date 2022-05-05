using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriggerDamageController : MonoBehaviour
{
    public PlayerData playerSO;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealthController>().PlayerTakesDamage(playerSO.currentHealth);
        }
    }
}

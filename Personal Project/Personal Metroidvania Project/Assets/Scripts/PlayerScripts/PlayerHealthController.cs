using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public PlayerData playerSO;
    

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    //deal damage to player
    public void PlayerTakesDamage(int damageAmount)
    {
        playerSO.currentHealth -= damageAmount;

        //what happens when player dies
        if (playerSO.currentHealth <= 0)
        {
            //prevent player health from reaching below 0
            playerSO.currentHealth = 0;

            if (transform.parent != null)
            {
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
            
        }
    }
}

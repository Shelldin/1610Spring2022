using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public PlayerData playerSO;

    public float iFrameLength;
    private float iFrameCounter;

    public float flashLength;
    private float flashCounter;

    public SpriteRenderer playerSprite;
    private bool colorIsWhite;

    public RespawnController respawnController;


    
    private void Start()
    {
        
        playerSO.currentHealth = playerSO.maxHealth;
        
        //update healthbar UI on start
        UIController.instance.UpdateHealthSlider(playerSO.currentHealth, playerSO.maxHealth);

        respawnController = FindObjectOfType<RespawnController>();
    }

    private void Update()
    {
        //countdown for iframe duration
        if (iFrameCounter > 0)
        {
            iFrameCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <=0)
            {
                if (playerSprite.color == Color.white)
                {
                    playerSprite.color = Color.green;
                }
                else
                {
                    playerSprite.color = Color.white;
                }
                flashCounter = flashLength;
            }

            if (iFrameCounter <= 0)
            {
                playerSprite.color = Color.white;
                flashCounter = 0;
            }
        }
        
    }

    //deal damage to player
    public void PlayerTakesDamage(int damageAmount)
    {
        //damage only happens when not in iFrames
        if (iFrameCounter <= 0)
        {
            playerSO.currentHealth -= damageAmount;

            //what happens when player dies
            if (playerSO.currentHealth <= 0)
            {
                //prevent player health from reaching below 0
                playerSO.currentHealth = 0;

                if (transform.parent != null)
                {
                    //transform.parent.gameObject.SetActive(false);
                    respawnController.Respawn();
                }
                else
                {
                    //gameObject.SetActive(false);
                    respawnController.Respawn();
                }
            }
            else
            {
                //reset iFrames
                iFrameCounter = iFrameLength;
            }

            //update health bar after taking damage
            UIController.instance.UpdateHealthSlider(playerSO.currentHealth, playerSO.maxHealth);
        }
    }

    
}

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

    public float iFrameLength;
    private float iFrameCounter;

    public float flashLength;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //set health to full on start
        currentHealth = maxHealth;
        
        //set health slider values
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
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
                foreach (SpriteRenderer spriteRenderer in playerSprites)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                }

                flashCounter = flashLength;
            }

            if (iFrameCounter <= 0)
            {
                foreach (SpriteRenderer spriteRenderer in playerSprites)
                {
                    spriteRenderer.enabled = true;
                }

                flashCounter = 0;
            }
        }
    }

    //deals damage to player
    public void DamagePlayer(int damageAmount)
    {
        //player won't take damage if it has iframes
        if (iFrameCounter <= 0)
        {
            currentHealth -= damageAmount;

            //if health reaches 0 or below, the player dies
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                
                RespawnController.instance.Respawn();
            }
            else
            {
                //give player iframes when taking damage
                iFrameCounter = iFrameLength;
            }

            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    //refill player health to max
    public void FillHealth()
    {
        currentHealth = maxHealth;
        
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}

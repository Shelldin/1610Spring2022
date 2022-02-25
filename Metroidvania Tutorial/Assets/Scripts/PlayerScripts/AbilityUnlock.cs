using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    private PlayerAbilityTracker player;
    
    //ability unlock variables
    public bool unlockDoubleJump,
        unlockDash,
        unlockBecomeBall,
        unlockDropBomb;
    
    //effect variables
    public GameObject pickupEffect;
    
    //unlock message variables
    public string unlockMessage;
    public TMP_Text unlockText;
    public float textDisplayTime = 3f;
    
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            
            //reference the ability tracker of the player
             player = col.GetComponentInParent<PlayerAbilityTracker>();
            
            //if statements determine which ability is being unlocked based on the ability variables are set to true
            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;
            }

            if (unlockDash)
            {
                player.canDash = true;
            }

            if (unlockBecomeBall)
            {
                player.canBecomeBall = true;
            }

            if (unlockDropBomb)
            {
                player.canDropBomb = true;
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);
            
            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);
            
            Destroy(unlockText.transform.parent.gameObject, textDisplayTime);
            
            Destroy(gameObject);
        }
    }
}

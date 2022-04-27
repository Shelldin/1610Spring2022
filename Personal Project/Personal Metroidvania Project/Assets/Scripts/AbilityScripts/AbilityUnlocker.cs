using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class AbilityUnlocker : MonoBehaviour
{
    public PlayerData playerSO;
    public ParticleSystem pickUpEffect;
    
    public bool unlockHover,
        unlockTeleport;

    public string unlockText;

    public TMP_Text uiText;

    //unlock designated abilities when triggered by player
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (unlockHover)
            {
                playerSO.hoverUnlocked = true;
            }

            if (unlockTeleport)
            {
                playerSO.teleportUnlocked = true;
            }
            Instantiate(pickUpEffect, transform.position, transform.rotation);

            uiText.transform.parent.SetParent(null);
            uiText.transform.parent.position = transform.position;
            
            uiText.text = unlockText;
            
            uiText.gameObject.SetActive(true);
            
            Destroy(uiText.transform.parent.gameObject, 5f);
            
            Destroy(gameObject);
        }
        
    }
}

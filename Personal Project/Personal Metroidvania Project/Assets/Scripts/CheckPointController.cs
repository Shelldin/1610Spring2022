using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public PlayerData playerSO;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerSO.SetRespawnPoint(gameObject.transform.position);
            anim.SetBool("isChecked", true);
        }
    }
}

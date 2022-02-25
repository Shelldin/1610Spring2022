using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D bulletRB;

    public Vector2 moveDir;

    public GameObject impactEffect;

    public int damageAmount = 1;

    private void Update()
    {
        //determine direction and speed of bullet
        bulletRB.velocity = moveDir * bulletSpeed;
    }

    //destroy bullet when it collides with another Collider2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if collider is an enemy and damages enemy if true
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }
        
        //particle effect on impact
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    //destroy bullet when not viewable by any camera
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

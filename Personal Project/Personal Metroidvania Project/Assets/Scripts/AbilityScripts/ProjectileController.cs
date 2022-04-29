using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public ProjectileData shotSO;
    
    public Rigidbody2D shotRB;

    public Vector2 moveDir;

    
    
    
    // Update is called once per frame
    void Update()
    {
        //projectile movement
        shotRB.velocity = moveDir * shotSO.shotSpeed;
        if (shotRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (shotRB.velocity.x < 0)
        {
            transform.localScale = Vector3.one;
        }
            
        
    }
    
    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.CompareTag("Enemy"))
        {
            trig.GetComponent<EnemyHealthController>().TakeDamage(shotSO.projectileDamage);
        }

        if (trig.CompareTag("Player"))
        {
            trig.GetComponent<PlayerHealthController>().PlayerTakesDamage(shotSO.projectileDamage);
        }
        
        if (shotSO.impactEffect != null)
        {
            //play impact effect on trigger
            Instantiate(shotSO.impactEffect, transform.position, Quaternion.identity);
        }

        //destroy on trigger
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        //destroy projectile when it leaves the screen
        Destroy(gameObject);
    }
}

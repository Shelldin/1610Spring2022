using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    //chasing variables
    public float rangeToStartChase;
    private bool isChasing;

    //movement variables
    public float moveSpeed,
        turnSpeed;

    //target to chase
    private Transform player;
    
    //animation
    public Animator anim;

    private void Start()
    {
        //assign player transform variable via the attached PlayerHealthController
        player = PlayerHealthController.instance.transform;
    }

    private void Update()
    {
        
        //checks distance between enemy flyer and player if isChasing = false
        if (!isChasing)
        {
            //if distance between player and enemy is less than rangeToStartChase sets isChasing to true
            if (Vector3.Distance(transform.position, player.position) < rangeToStartChase)
            {
                isChasing = true;
                
                anim.SetBool("isChasing", isChasing);
            }
        }
        //when isChasing is true, begin chasing movement
        else
        {
            if (player.gameObject.activeSelf)
            {
                //math voodoo to get the direction and angle to rotate enemy towards player
                Vector3 direction = transform.position - player.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed*Time.deltaTime);

                //move flying enemy based on direction it's facing
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
            }
        }
    }
}

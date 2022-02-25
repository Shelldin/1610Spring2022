using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    //patrol point variables
    public Transform[] patrolPoints;
    private int currentPoint;

    //patrol movement variables
    public float moveSpeed,
        waitAtPointTime;
     private float waitTimeCounter;

     //jumping variables
    public float jumpForce;
    public Transform groundPoint;
    public LayerMask groundLayers;
    private bool isGrounded;

    public Rigidbody2D enemyRB;
    
    //animation variables
    public Animator anim;
    

    private void Start()
    {
        waitTimeCounter = waitAtPointTime;

        foreach (Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    private void Update()
    {
        //check if enemy is on ground
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, groundLayers);
        
        //moving to next patrol point
        //check distance between patrol point and enemy and move if distance is too great
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                //right movement
                enemyRB.velocity = new Vector2(moveSpeed, enemyRB.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                //left movement
                enemyRB.velocity = new Vector2(-moveSpeed, enemyRB.velocity.y);
                transform.localScale = Vector3.one;
            }

            if (transform.position.y < patrolPoints[currentPoint].position.y -.5f && isGrounded)
            {
                enemyRB.velocity = new Vector2(enemyRB.velocity.x, jumpForce);
            }
        }
        //waiting when reaching a patrol point
        else
        {
            enemyRB.velocity = new Vector2(0f, enemyRB.velocity.y);

            waitTimeCounter -= Time.deltaTime;
            
            //reset time to wait and target next patrol point to move to
            if (waitTimeCounter <= 0 )
            {
                waitTimeCounter = waitAtPointTime;

                currentPoint++;
                
                //resetting the current patrol point if the length is reached
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }
        anim.SetFloat("speed", Mathf.Abs(enemyRB.velocity.x));
    }
}

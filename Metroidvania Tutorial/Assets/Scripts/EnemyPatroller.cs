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

    public float jumpForce;

    public Rigidbody2D enemyRB;

    private void Start()
    {
        waitTimeCounter = waitAtPointTime;
    }

    private void Update()
    {
        //check distance between patrol point and enemy and move if distance is too great
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                //right movement
                enemyRB.velocity = new Vector2(moveSpeed, enemyRB.velocity.y);
            }
            else
            {
                //left movement
                enemyRB.velocity = new Vector2(-moveSpeed, enemyRB.velocity.y);
            }
        }
    }
}

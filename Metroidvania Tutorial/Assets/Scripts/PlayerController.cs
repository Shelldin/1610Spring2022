using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//from Metroidvania Tutorial on Udemy https://www.udemy.com/course/unity-metvania/
public class PlayerController : MonoBehaviour
{
        public Rigidbody2D playerRB;

        public float moveSpeed;
        public float jumpForce;

        public Transform groundPoint;
        public LayerMask groundLayers;
        private bool isOnGround;
        

        private void Update()
        {
                playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*moveSpeed, playerRB.velocity.y);

                isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, groundLayers);

                if (Input.GetButtonDown("Jump") && isOnGround)
                {
                        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                }
        }
}

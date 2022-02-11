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

        public Animator anim;


        public BulletController shotToFire;
        public Transform shotPoint;
        
        private bool canDoubleJump;

        private void Update()
        {
                //horizontal movement
                playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*moveSpeed, playerRB.velocity.y);
                
                //controlling direction of the character
                if (playerRB.velocity.x < 0)
                {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if(playerRB.velocity.x > 0)
                {
                        transform.localScale = Vector3.one;
                }

                //checking if player is on ground or not
                isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, groundLayers);

                //Jumping
                if (Input.GetButtonDown("Jump") && (isOnGround || canDoubleJump))
                {
                        if (isOnGround)
                        { 
                                canDoubleJump = true; 
                        }
                        else
                        {
                                canDoubleJump = false;
                        }
                                
                        
                        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                }


                //firing bullet prefab from player and determining direction of bullet
                if (Input.GetButtonDown("Fire1"))
                {
                        Instantiate(shotToFire, shotPoint.position, shotPoint.rotation)
                                .moveDir = new Vector2(transform.localScale.x, 0);
                        
                        anim.SetTrigger("shotFired");
                }
                
                
                anim.SetBool("isOnGround", isOnGround);
                anim.SetFloat("speed", Mathf.Abs(playerRB.velocity.x));
        }
}

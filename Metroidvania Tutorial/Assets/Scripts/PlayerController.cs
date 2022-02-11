using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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

        public float dashSpeed,
                dashTime;
        private float dashCounter;

        public float waitAfterDashing;
        private float dashRechargeCounter;

        public SpriteRenderer playerSR,
                afterImage;

        public float afterImageLifeTime,
                timeBetweenAfterImages;
        private float afterImageCounter;
        public Color afterImageColor;


        private void Update()
        {
                // Player must wait a short time between dashing to dash again
                if (dashRechargeCounter > 0)
                {
                        dashRechargeCounter -= Time.deltaTime;
                }

                else
                {

                        //checks if player is dashing
                        if (Input.GetButtonDown("Fire2"))
                        {
                                dashCounter = dashTime;

                                ShowAfterImage();
                        }

                }


                //dash movement
                if (dashCounter > 0)
                {
                        //resolving duration of dash
                        dashCounter -= Time.deltaTime;

                        playerRB.velocity = new Vector2(dashSpeed * transform.localScale.x, 0);

                        afterImageCounter -= Time.deltaTime;
                        if (afterImageCounter <= 0)
                        {
                                ShowAfterImage();
                        }
                        
                        
                        //reset dashRechargeCounter while player is in a dash
                        dashRechargeCounter = waitAfterDashing;
                }
                //if player is not dashing they can move
                //(player does not have horizontal movement control during a dash)
                else
                {

                        //horizontal movement
                        playerRB.velocity =
                                new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, playerRB.velocity.y);

                        //controlling direction of the character
                        if (playerRB.velocity.x < 0)
                        {
                                transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        else if (playerRB.velocity.x > 0)
                        {
                                transform.localScale = Vector3.one;
                        }

                }
                

                //checking if player is on ground or not
                isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, groundLayers);

                //Jumping
                if (Input.GetButtonDown("Jump") && (isOnGround || canDoubleJump))
                {
                        //Resolving double jump attempts
                        if (isOnGround)
                        { 
                                canDoubleJump = true; 
                        }
                        else
                        {
                                canDoubleJump = false;
                                
                                anim.SetTrigger("doubleJump");
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

        //function to create after images when dashing
        public void ShowAfterImage()
        {
           SpriteRenderer image =  Instantiate(afterImage, transform.position, transform.rotation);
           image.sprite = playerSR.sprite;
           image.transform.localScale = transform.localScale;
           image.color = afterImageColor;
           
           Destroy(image.gameObject, afterImageLifeTime);

           afterImageCounter = timeBetweenAfterImages;
        }
}

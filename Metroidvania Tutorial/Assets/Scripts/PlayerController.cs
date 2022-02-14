using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
//from Metroidvania Tutorial on Udemy https://www.udemy.com/course/unity-metvania/
public class PlayerController : MonoBehaviour
{
        public Rigidbody2D playerRB;
        
        //general movement variables
        public float moveSpeed;
        
        //Jump Variables
        public float jumpForce;
        
        public Transform groundPoint;
        public LayerMask groundLayers;
        private bool isOnGround;
        
        
        //general animation variables
        public Animator anim;

        //Shooting Variables
        public BulletController shotToFire;
        public Transform shotPoint;
        
        //Double Jump Variables
        private bool canDoubleJump;
        
        //dashing variables
        public float dashSpeed,
                dashTime;
        private float dashCounter;

        public float waitAfterDashing;
        private float dashRechargeCounter;

        //after image effect variables
        public SpriteRenderer playerSR,
                afterImage;
        
        public float afterImageLifeTime,
                timeBetweenAfterImages;
        private float afterImageCounter;
        public Color afterImageColor;
        
        //morph ball variables
        public GameObject standing,
                ball;

        public float waitToBall;
        private float ballCounter;
        public Animator ballAnim;
        
        //bomb variables
        public Transform bombPoint;
        public GameObject bomb;
        


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
                        if (Input.GetButtonDown("Fire2") && standing.activeSelf)
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


                //shooting and dropping bombs
                if (Input.GetButtonDown("Fire1"))
                {
                        //fire bullets if standing
                        if (standing.activeSelf)
                        {
                                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation)
                                        .moveDir = new Vector2(transform.localScale.x, 0);
                        
                                anim.SetTrigger("shotFired");    
                        }
                        //drop bombs if in morph ball
                        else if(ball.activeSelf)
                        {
                                Instantiate(bomb, bombPoint.position, bombPoint.rotation);
                        }
                        
                }
                
                //morph ball
                if (!ball.activeSelf)
                {
                        //swap from standing to morph ball
                        if (Input.GetAxisRaw("Vertical")< -.9f)
                        {
                                ballCounter -= Time.deltaTime;
                                if (ballCounter <= 0)
                                {
                                     ball.SetActive(true);
                                     standing.SetActive(false);
                                }
                        }
                        //resetting waitToBall when not switching from one mode to the other
                        else
                        {
                                ballCounter = waitToBall;
                        }
                }
                else
                {
                        //swap from morph ball to standing
                        if (Input.GetAxisRaw("Vertical")> .9f)
                        {
                                ballCounter -= Time.deltaTime;
                                if (ballCounter <= 0)
                                {
                                        ball.SetActive(false);
                                        standing.SetActive(true);
                                }
                        }
                        //resetting waitToBall when not switching from one mode to the other
                        else
                        {
                                ballCounter = waitToBall;
                        } 
                }
                
                //standing animations
                if (standing.activeSelf)
                {
                        anim.SetBool("isOnGround", isOnGround);
                        anim.SetFloat("speed", Mathf.Abs(playerRB.velocity.x));
                }
                
                //ball animations
                if (ball.activeSelf)
                {
                        ballAnim.SetFloat("speed", Mathf.Abs(playerRB.velocity.x));
                }
                
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerSO;
    
    public Rigidbody2D playerRB;

    public Transform groundPoint;
    public LayerMask groundLayer;

    public Animator anim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSO.moveSpeed, playerRB.velocity.y);
        
        //flip direction based on horizontal input
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //checking if on ground
        playerSO.isOnGround = Physics2D.OverlapCircle(groundPoint.position, .3f, groundLayer);
        
        //jumping
        if (Input.GetButtonDown("Jump") && playerSO.isOnGround)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerSO.jumpForce);
        }
        
        
        //jump animation
        anim.SetBool("isOnGround", playerSO.isOnGround);
        //run animation
        anim.SetFloat("speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerSO;
    
    public Rigidbody2D playerRB;

    public Transform groundPoint;
    public LayerMask groundLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSO.moveSpeed, playerRB.velocity.y);

        playerSO.isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, groundLayer);
        
        //jumping
        if (Input.GetButtonDown("Jump") && playerSO.isOnGround)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerSO.jumpForce);
        }
    }
}

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

    public Transform shotPoint;

    private float originalGravity;

    public Transform teleportPoint;

    private bool isTeleporting = false;

    
    
    // Start is called before the first frame update
    void Start()
    {
        originalGravity = playerRB.gravityScale;
        playerSO.canTeleport = true;
        playerSO.canHover = true;
        playerSO.canShoot = true;

        playerSO.respawnPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //teleport if player hasn't hovered and teleport is unlocked
        if (Input.GetButtonDown("Fire2") && playerSO.canHover && playerSO.teleportUnlocked && playerSO.canTeleport)
        {
            StartCoroutine(TeleportCoroutine(playerSO.teleDuration));
        }
  
        
        
        if (!isTeleporting)
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

            //check if player has unlocked the hover upgrade
            if (playerSO.hoverUnlocked)
            {
                //hover for a limited time while the jump button is held while already in the air
                if (Input.GetButtonDown("Jump") && !playerSO.isOnGround && playerSO.canHover)
                {

                    StartCoroutine(HoverCoroutine(playerSO.hoverDuration));

                }
            }
        }

        //resetting hover when grounded
        if (playerSO.isOnGround)
        {
            playerSO.canHover = true;
        }

        //shoot active projectile
        if (Input.GetButtonDown("Fire1") && playerSO.canShoot)
        {
            Instantiate(playerSO.activeProjectile, shotPoint.position, shotPoint.rotation).moveDir =
                new Vector2(transform.localScale.x, 0);
            StartCoroutine(ShootingCooldownCoroutine(playerSO.timeBetweenShots));
        }
        
        
        //jump animation
        anim.SetBool("isOnGround", playerSO.isOnGround);
        //run animation
        anim.SetFloat("speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }

    //Hover functionality countdown the hover duration when hover is used
    private IEnumerator HoverCoroutine(float countdownTime)
    {
        playerSO.canHover = false;
        //stops player at the y position when hover is activated
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
        //no gravity while hover is active
        playerRB.gravityScale = 0;
        yield return new WaitForSeconds(countdownTime);
        //reset gravity when duration ends
        playerRB.gravityScale = originalGravity;
        
    }

    //teleport functionality
    private IEnumerator TeleportCoroutine(float countdownTime)
    {
        playerSO.canTeleport = false;
        isTeleporting = true;
        //halt gravity and all momentum while teleporting
        playerRB.velocity = Vector2.zero;
        playerRB.gravityScale = 0;
        //disappear animations
        anim.SetTrigger("disappear");
        anim.SetBool("isVisible", false);
        yield return new WaitForSeconds(1f);
        //check if player will hit a collider on the ground layer
        Debug.DrawRay(transform.position,transform.TransformDirection(transform.localScale.x, 0,0)*10f,Color.red, 3f);
        RaycastHit2D teleHit = Physics2D.Raycast(transform.position,
            transform.TransformDirection(transform.localScale.x, 0, 0),
            Mathf.Abs(teleportPoint.position.x - transform.position.x), groundLayer);
        //teleport to point of collision if player would collide or to teleport point if no collision occurs
        if (teleHit)
        {
            transform.position = teleHit.point;
        }
        else
        {
            transform.position = teleportPoint.position;
        }
        yield return new WaitForSeconds(.2f);
        //reappear animation and reset gravity
        anim.SetBool("isVisible", true);
        playerRB.gravityScale = originalGravity;
        isTeleporting = false;
        //time delay so player can't spam teleports
        yield return new WaitForSeconds(playerSO.timeBetweenTeleports);
        playerSO.canTeleport = true;

    }

    //coroutine to determine player firing speed
    private IEnumerator ShootingCooldownCoroutine(float timeBetweenShots)
    {
        playerSO.canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        playerSO.canShoot = true;

    }

    
}

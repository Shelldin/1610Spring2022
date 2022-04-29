using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDemonController : MonoBehaviour
{
    public EnemyData demonSO;
    public Animator anim;
    public Transform shotPoint;
    public Transform pillarCheckPoint;
    public LayerMask playerLayer;
    public float pillarCheckRadius;
    public AnimationClip fireballClip;
    public Renderer enemyRenderer;

    private bool canShoot;
    private bool pillarActive;
    
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
          
    }

    // Update is called once per frame
    void Update()
    {
        //sets pillarActive based on if the player is in range or not;
        pillarActive = Physics2D.OverlapCircle(pillarCheckPoint.position, pillarCheckRadius,
            playerLayer);

        //fire pillar
        if (pillarActive)
        {
            StopCoroutine(ShootFireballCoroutine(demonSO.timeBetweenFireballs));
            canShoot = true;
            anim.SetBool("pillarActive", pillarActive);
        }
        else
        {
            anim.SetBool("pillarActive", pillarActive);
            //shoot fireballs on a loop when the demon is on screen
            if (canShoot && enemyRenderer.isVisible)
            {
                StartCoroutine(ShootFireballCoroutine(demonSO.timeBetweenFireballs));
            }  
        }
        
        
    }

    

    //coroutine to shoot fireballs
    private IEnumerator ShootFireballCoroutine(float timeBetweenShots)
    {
        canShoot = false;
        anim.SetTrigger("fireballTrig");
        //instantiate the fireball prefab halfway through the shooting animation
        yield return new WaitForSeconds(fireballClip.length*.5f);
        Instantiate(demonSO.activeProjectile,shotPoint.position, shotPoint.rotation).moveDir =
            new Vector2(transform.localScale.x, 0);
        //create delay between fireball shots
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
}

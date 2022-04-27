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
    
    // Start is called before the first frame update
    void Start()
    {
        demonSO.canShoot = true;
        Debug.Log(fireballClip.length);   
    }

    // Update is called once per frame
    void Update()
    {
        //sets pillarActive based on if the player is in range or not;
        demonSO.pillarActive = Physics2D.OverlapCircle(pillarCheckPoint.position, pillarCheckRadius,
            playerLayer);

        //fire pillar
        if (demonSO.pillarActive)
        {
            StopCoroutine(ShootFireballCoroutine(demonSO.timeBetweenFireballs));
            demonSO.canShoot = true;
            anim.SetBool("pillarActive", demonSO.pillarActive);
        }
        else
        {
            anim.SetBool("pillarActive", demonSO.pillarActive);
            //shoot fireballs on a loop when the demon is on screen
            if (demonSO.canShoot && enemyRenderer.isVisible)
            {
                StartCoroutine(ShootFireballCoroutine(demonSO.timeBetweenFireballs));
            }  
        }
        
        
    }

    //coroutine to shoot fireballs
    private IEnumerator ShootFireballCoroutine(float timeBetweenShots)
    {
        demonSO.canShoot = false;
        anim.SetTrigger("fireballTrig");
        //instantiate the fireball prefab halfway through the shooting animation
        yield return new WaitForSeconds(fireballClip.length*.5f);
        Instantiate(demonSO.activeProjectile,shotPoint.position, shotPoint.rotation).moveDir =
            new Vector2(transform.localScale.x, 0);
        //create delay between fireball shots
        yield return new WaitForSeconds(timeBetweenShots);
        demonSO.canShoot = true;
    }
}

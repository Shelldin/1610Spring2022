using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBattle : MonoBehaviour
{
    private CameraController cam;

    public Transform camPosition;

    public float camSpeed;

    public int phase2Int,
        phase3Int;

    public float acitveTime,
        fadeOutTime,
        inactiveTime;

    private float activeCounter,
        fadeCounter,
        inactiveCounter;

    public Transform[] spawnPoints;
    private Transform targetPoint;
    public float moveSpeed;

    public Animator anim;

    public Transform boss;

    public float slowTimeBetweenShots, 
        fastTimeBetweenShots;

    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;

    public GameObject winObjects;

    private bool battleEnd;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraController>();

        //disabling CameraController so it stops following player during boss battle
        cam.enabled = false;

        activeCounter = acitveTime;

        shotCounter = slowTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position =
            Vector3.MoveTowards(cam.transform.position, camPosition.position, camSpeed * Time.deltaTime);


        if (!battleEnd)
        {
            
       
            //phase 1 boss behavior
            if (BossHealthContoller.instance.currentHealth> phase2Int)
            {
                if (activeCounter> 0)
                {
                
                    //countdown before fading out
                    activeCounter -= Time.deltaTime;
                
                    //reset fadeCounter
                    if (activeCounter <= 0)
                    {
                        fadeCounter = fadeOutTime;
                        anim.SetTrigger("vanish");
                    }
                
                    //fireball shooting behavior
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = slowTimeBetweenShots;

                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    }
                }
            
                //fading out after activeCounter reaches 0
                else if(fadeCounter > 0)
                {
                    //fade out countdown
                    fadeCounter -= Time.deltaTime;
                
                    //set boss inactive while faded out and reset inactiveCounter
                    if (fadeCounter <= 0)
                    {
                        boss.gameObject.SetActive(false);

                        inactiveCounter = inactiveTime;
                    }
                }
            
                //being inactive after fadeCounter reaches 0
                else if (inactiveCounter>0)
                {
                
                    //inactive countdown
                    inactiveCounter -= Time.deltaTime;
                
                    //re-activate boss at new spawn point when inactiveCounter reaches 0
                    if (inactiveCounter <= 0)
                    {
                        boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        boss.gameObject.SetActive(true);

                        //reset activeCounter
                        activeCounter = acitveTime;

                        //reset shotCounter
                        shotCounter = slowTimeBetweenShots;
                    }
                }
            }
            //phase 2 & 3 behavior
            else
            {
            
                //set target point to boss's position if targetPoint is null
                if (targetPoint == null)
                {
                    targetPoint = boss;
                    fadeCounter = fadeOutTime;
                    anim.SetTrigger("vanish");
                }
            
            
                else
                {
                
                    //if boss isn't at current target point, move towards target point
                    if (Vector3.Distance(boss.position, targetPoint.position) > .02f)
                    {
                        boss.position =
                            Vector3.MoveTowards(boss.position, targetPoint.position, moveSpeed * Time.deltaTime);
                    
                        //when boss reaches target point reset fadeCounter and trigger vanish animation
                        if (Vector3.Distance(boss.position, targetPoint.position) <= .02f)
                        {
                            fadeCounter = fadeOutTime;
                            anim.SetTrigger("vanish");
                        }
                    
                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            //while in phase 2 health range use slower shots time
                            if (BossHealthContoller.instance.currentHealth > phase3Int)
                            {
                                shotCounter = slowTimeBetweenShots;
                            }
                            //if below phase 2 health range use fast shot time (phase 3)
                            else
                            {
                                shotCounter = fastTimeBetweenShots;
                            }

                            Instantiate(bullet, shotPoint.position, Quaternion.identity);
                        }
                    }

                    //fade out behavior
                    else if(fadeCounter > 0)
                    {
                    
                        //fade out countdown
                        fadeCounter -= Time.deltaTime;
                    
                        //when fadeCounter reaches 0 reset set boss inactive and reset inactiveCounter
                        if (fadeCounter <= 0)
                        {
                            boss.gameObject.SetActive(false);

                            inactiveCounter = inactiveTime;
                        }
                    }
                
                    //inacitve behaviour
                    else if (inactiveCounter>0)
                    {
                        //inactive countdown
                        inactiveCounter -= Time.deltaTime;
                    
                        //when inactiveCounter reaches 0 boss reappears at new point and selects new target position
                        if (inactiveCounter <= 0)
                        {
                            boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                            int whileBreaker = 0;
                        
                            //choose new targetpoint if boss selects the target point he's already at
                            while (targetPoint.position == boss.position && whileBreaker < 100)
                            {
                                targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                                whileBreaker++;
                            }
                        
                            //boss reappears
                            boss.gameObject.SetActive(true);
                        
                            //while in phase 2 health range use slower shots time
                            if (BossHealthContoller.instance.currentHealth > phase3Int)
                            {
                                shotCounter = slowTimeBetweenShots;
                            }
                            //if below phase 2 health range use fast shot time (phase 3)
                            else
                            {
                                shotCounter = fastTimeBetweenShots;
                            }
                        
                        }
                    }
                }
            }
        }
        else
        {
            //fade out countdown
            fadeCounter -= Time.deltaTime;

            
            //when boss has vanished
            if (fadeCounter < 0 )
            {
                //spawn win objects and make them no longer a child of the boss so they dont get deactivated with the boss
                if (winObjects != null)
                {
                    winObjects.SetActive(true);
                    winObjects.transform.SetParent(null);
                }
                
                
                //re-enable normal camera movement
                cam.enabled = true;
                
                //deactivate the boss
                gameObject.SetActive(false);
            }
        }
    }

    //end of battle behaviour
    public void EndBattle()
    {
        battleEnd = true;

        //Rest fadeCounter
        fadeCounter = fadeOutTime;
        anim.SetTrigger("vanish");

        //disable boss collider to prevent further interaction
        boss.GetComponent<Collider2D>().enabled = false;

        //find and clear any remaining active boss bullets
        BossBullet[] bullets = FindObjectsOfType<BossBullet>();
        if (bullets.Length > 0)
        {
            foreach (BossBullet bossBullet in bullets)
            {
                Destroy(bossBullet.gameObject);
            }
        }
    }
}

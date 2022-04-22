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

        //phase 1 boss behavior
        if (BossHealthContoller.instance.currentHealth> phase2Int)
        {
            if (activeCounter> 0)
            {
                activeCounter -= Time.deltaTime;
                if (activeCounter <= 0)
                {
                    fadeCounter = fadeOutTime;
                    anim.SetTrigger("vanish");
                }
                
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = slowTimeBetweenShots;

                    Instantiate(bullet, shotPoint.position, Quaternion.identity);
                }
            }
            else if(fadeCounter > 0)
            {
                fadeCounter -= Time.deltaTime;
                if (fadeCounter <= 0)
                {
                    boss.gameObject.SetActive(false);

                    inactiveCounter = inactiveTime;
                }
            }
            else if (inactiveCounter>0)
            {
                inactiveCounter -= Time.deltaTime;
                if (inactiveCounter <= 0)
                {
                    boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    boss.gameObject.SetActive(true);

                    activeCounter = acitveTime;

                    shotCounter = slowTimeBetweenShots;
                }
            }
        }
        //phase 2 behavior
        else
        {
            if (targetPoint == null)
            {
                targetPoint = boss;
                fadeCounter = fadeOutTime;
                anim.SetTrigger("vanish");
            }
            else
            {
                if (Vector3.Distance(boss.position, targetPoint.position) > .02f)
                {
                    boss.position =
                        Vector3.MoveTowards(boss.position, targetPoint.position, moveSpeed * Time.deltaTime);
                    
                    if (Vector3.Distance(boss.position, targetPoint.position) <= .02f)
                    {
                        fadeCounter = fadeOutTime;
                        anim.SetTrigger("vanish");
                    }
                }
                else if(fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        boss.gameObject.SetActive(false);

                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter>0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                        targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                        int whileBreaker = 0;
                        while (targetPoint.position == boss.position && whileBreaker < 100)
                        {
                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                            whileBreaker++;
                        }
                        
                        boss.gameObject.SetActive(true);
                        
                    }
                }
            }
        }
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }
}

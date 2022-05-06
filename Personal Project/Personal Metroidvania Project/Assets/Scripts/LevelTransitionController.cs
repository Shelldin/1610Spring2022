using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionController : MonoBehaviour
{
    public PlayerData playerSO;

    private bool playerTransitioning;

    public string levelToLoad;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Player").GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetAxisRaw("Vertical") < 0 && playerSO.canMove && !playerTransitioning)
        {
            playerSO.canMove = false;

            StartCoroutine(LevelTransitionCoroutine());
        }
    }

    private IEnumerator LevelTransitionCoroutine()
    {
        playerTransitioning = true;
        
        anim.SetTrigger("outro");
        
        yield return new WaitForSeconds(2.3f);
        
        UIController.instance.StartFade();

        yield return new WaitForSeconds(1.5f);
        
        UIController.instance.EndFade();

        playerSO.canMove = true;

        SceneManager.LoadScene(levelToLoad);
    }
    
}

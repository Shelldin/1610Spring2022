using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector3 respawnPoint;

    public float waitToRespawn;

    private GameObject player;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.gameObject;

        respawnPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawn(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    //start the respawn coroutine
    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    //respawn the player after they die
    IEnumerator RespawnCoroutine()
    {
        player.SetActive(false);
        if (deathEffect != null)
        {
            Instantiate(deathEffect, player.transform.position, player.transform.rotation);
        }

        yield return new WaitForSeconds(waitToRespawn);

        //reloads scene to respawn enemies
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //restarts player position
        player.transform.position = respawnPoint;
        player.SetActive(true);
        
        //refill player health
        PlayerHealthController.instance.FillHealth();
    }
}

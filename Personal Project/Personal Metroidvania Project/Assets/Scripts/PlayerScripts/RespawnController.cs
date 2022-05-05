using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public PlayerData playerSO;

    private float waitToRespawn = 2f;

    public GameObject playerObj;
    
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }
    
    private IEnumerator RespawnCoroutine()
    {
        playerObj.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);

        playerObj.transform.position = playerSO.respawnPoint;
        playerSO.RefillHealth();
        UIController.instance.UpdateHealthSlider(playerSO.currentHealth, playerSO.maxHealth);
        playerObj.SetActive(true);
    }
}

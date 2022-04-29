using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePIllarDamageController : MonoBehaviour
{
    public EnemyData demonSO;
    
    //fire pillar deals damage
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealthController>().PlayerTakesDamage(demonSO.firePillarDamage);
        }
    }
}

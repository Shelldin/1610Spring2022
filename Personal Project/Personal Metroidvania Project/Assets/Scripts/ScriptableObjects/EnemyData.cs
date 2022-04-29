using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public int healthTotal;
    public int fireballDamage;
    public int firePillarDamage;

    public float timeBetweenFireballs = 3f;

    public ProjectileController activeProjectile;

    
   
   
}

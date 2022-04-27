using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public float health;
    public float fireballDamage;
    public float firePillarDamage;

    public float timeBetweenFireballs;
}

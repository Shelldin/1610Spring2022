using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public int projectileDamage;
    
    public float shotSpeed;
    
    public GameObject impactEffect;


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed;

    public float jumpForce;

    public float teleportSpeed;

    public float teleDuration = .2f;

    public float timeBetweenTeleports = .5f;
    

    public ProjectileController activeProjectile;

    [HideInInspector] 
    public bool isOnGround,
        canHover,
        canTeleport;

    public float hoverDuration;

    public bool hoverUnlocked,
        teleportUnlocked;

}
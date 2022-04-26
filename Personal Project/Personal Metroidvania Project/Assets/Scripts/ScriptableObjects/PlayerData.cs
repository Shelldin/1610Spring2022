using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed;

    public float jumpForce;

    [HideInInspector]
    public bool isOnGround;
    
}

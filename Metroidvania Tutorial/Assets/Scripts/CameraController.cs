using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;
    public BoxCollider2D cameraBoundsBox;

    private float halfHeight,
        halfWidth;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    private void Update()
    {
        //following player position with camera
        if (player != null)
        {
            transform.position =new Vector3(
                Mathf.Clamp(player.transform.position.x, cameraBoundsBox.bounds.min.x+halfWidth, cameraBoundsBox.bounds.max.x-halfWidth),
                Mathf.Clamp(player.transform.position.y, cameraBoundsBox.bounds.min.y+halfHeight, cameraBoundsBox.bounds.max.y-halfHeight),
                transform.position.z );
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;

    public BoxCollider2D camBoundBox;

    private float camHalfHeight,
        camHalfWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        // orthographicSize is half of the "Size" field on the camera in the inspector
        camHalfHeight = Camera.main.orthographicSize;
        //multiplying the halfHeight by the aspect ratio (which is the height divided by the width)
        camHalfWidth = camHalfHeight * Camera.main.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        /*move camera to follow player.
        Mathf.Clamp restricts a value between two other values.
        using Mathf.Clamp to not let the camera move beyond the camBoundBox's BoxCollider.
        the halfWidth and halfHeight are used to adjust the camera's position so that the edges stop
        at the camBoundBox's box collider instead of the center of the camera.
        */
        transform.position =
            new Vector3(Mathf.Clamp(player.transform.position.x, camBoundBox.bounds.min.x + camHalfWidth,
                    camBoundBox.bounds.max.x - camHalfWidth),
                Mathf.Clamp(player.transform.position.y, camBoundBox.bounds.min.y + camHalfHeight,
                    camBoundBox.bounds.max.y - camHalfHeight),
                transform.position.z);
        
    }
}

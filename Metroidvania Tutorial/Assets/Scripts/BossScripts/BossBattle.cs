using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    private CameraController cam;

    public Transform camPosition;

    public float camSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraController>();

        //disabling CameraController so it stops following player during boss battle
        cam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position =
            Vector3.MoveTowards(cam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }
}

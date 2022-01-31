using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Destroy(GetComponent<MeshRenderer>());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOther : MonoBehaviour
{
    public GameObject other;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Destroy(other);
        }
    }
}

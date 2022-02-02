using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataType : MonoBehaviour
{
    private void Start()
    {
        Vector3 pos = transform.position;
        pos = new Vector3(0, 3, 0);

        Transform trans = transform;
        trans.position = new Vector3(0, 3, 0);
    }
}

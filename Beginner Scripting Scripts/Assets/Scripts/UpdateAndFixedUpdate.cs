using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class UpdateAndFixedUpdate : MonoBehaviour
{
    private void FixedUpdate()
    {
        Debug.Log("Fixed Update time: " +Time.deltaTime);
    }

    private void Update()
    {
        Debug.Log("Update time: " + Time.deltaTime);
    }
}

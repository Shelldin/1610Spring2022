using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForLoops : MonoBehaviour
{
    private int someNumber = 3;

    private void Start()
    {
        for (int i = 0; i < someNumber; i++)
        {
            Debug.Log("Some number is:" +i);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileLoop : MonoBehaviour
{
    private int fishThatAreCooked = 4;

    private void Start()
    {
        while (fishThatAreCooked > 0)
        {
            Debug.Log("A fish has been cooked");
            fishThatAreCooked--;
        }
    }
}

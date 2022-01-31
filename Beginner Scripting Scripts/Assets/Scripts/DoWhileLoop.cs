using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoWhileLoop : MonoBehaviour
{
    private void Start()
    {
        bool shouldContinue = false;

        do
        {
            print("Hello World!");
        } while (shouldContinue == true);
    }
}

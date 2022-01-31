using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesAndFunctions : MonoBehaviour
{
    private int someInt = 5;

    private void Start()
    {
        someInt = MultiplyByThree(someInt);
        Debug.Log(someInt);
    }

    int MultiplyByThree(int number)
    {
        int result;
        result = number * 3;
        return result;
    }
}
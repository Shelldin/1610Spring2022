using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEachLoop : MonoBehaviour
{
    private void Start()
    {
        string[] strings = new string[3];

        strings[0] = "string 1";
        strings[1] = "strings 2";
        strings[2] = "strings 3";

        foreach (var item in strings)
        {
            print(item);
        }
    }
}

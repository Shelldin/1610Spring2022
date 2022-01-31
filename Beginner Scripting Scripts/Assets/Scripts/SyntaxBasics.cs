using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyntaxBasics : MonoBehaviour
{
    private void Start()
    {
       //super fun commenting
       
       /* More exciting commenting fun
        commenting some more
        */
       Debug.Log(transform.position.x);

       if (transform.position.y <= 7f)
       {
           Debug.Log("WEEEEEEE!!!!");
       }
    }
}

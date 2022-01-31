using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckState : MonoBehaviour
{
    public GameObject myObj;

    private void Start()
    {
        Debug.Log("active self: "+ myObj.activeSelf);
        Debug.Log("active in hierarchy" + myObj.activeInHierarchy);
    }
}

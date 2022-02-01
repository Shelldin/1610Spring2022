using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingOtherComponents : MonoBehaviour
{
    public GameObject otherGameObject;

    private ExtraScript extraScript;
    private ExtraScriptTwo extraScriptTwo;
    private BoxCollider boxCol;

    private void Awake()
    {
        extraScript = GetComponent<ExtraScript>();
        extraScriptTwo = otherGameObject.GetComponent<ExtraScriptTwo>();
        boxCol = otherGameObject.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        boxCol.size = new Vector3(3, 3, 3);
        Debug.Log("Score is " + extraScript.playerScore);
        Debug.Log("You have died " + extraScriptTwo.numberOfPlayerDeaths + "times");
    }
}

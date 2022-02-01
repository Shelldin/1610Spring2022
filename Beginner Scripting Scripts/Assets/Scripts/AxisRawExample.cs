using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisRawExample : MonoBehaviour
{
    public float range;
    public Text textOutput;

    private void Update()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float xPos = horiz * range;

        transform.position = new Vector3(xPos, 2f, 0);
        textOutput.text = "Returned: " + horiz.ToString("F2");
    }
}

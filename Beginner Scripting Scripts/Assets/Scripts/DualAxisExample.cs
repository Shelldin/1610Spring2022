using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DualAxisExample : MonoBehaviour
{
   public Text horizontalValueDisplayText;
   public Text verticalValueDisplayText;
   public float hRange;
   public float vRange;

   private void Update()
   {
      float horiz = Input.GetAxis("Horizontal");
      float verti = Input.GetAxis("Vertical");
      float xPos = horiz * hRange;
      float yPos = verti * vRange;

      transform.position = new Vector3(xPos, 0, yPos);
      horizontalValueDisplayText.text = horiz.ToString("F2");
      verticalValueDisplayText.text = verti.ToString("F2");
   }
}

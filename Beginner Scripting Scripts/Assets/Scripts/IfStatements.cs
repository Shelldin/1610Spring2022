using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfStatements : MonoBehaviour
{
   private float coffeeTemprature = 80.0f;
   private float hotTempLimit = 75.0f;
   private float coldTempLimit = 45.0f;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
         TemperatureTest();

      coffeeTemprature -= Time.deltaTime * 5f;
   }

   void TemperatureTest()
   {
      if (coffeeTemprature > hotTempLimit)
      {
         print("too hot");
      }
      else if(coffeeTemprature < coldTempLimit)
      {
         print("too cold");
      }
      else
      {
         print("ideal temp achieved");
      }
   }
}

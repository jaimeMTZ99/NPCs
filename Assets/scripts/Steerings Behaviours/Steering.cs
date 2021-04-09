using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{

    public float angular;   //Velocidad angular
    public Vector3 linear;  //Vector de velocidad --> (Speed + direction)

    public Steering()
   {
      angular = 0f;
      linear = Vector3.zero;
   }
}

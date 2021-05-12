using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punto : Vertex
{
    public Equipo equipo;
    public float valor = 0f;


    public bool SetValue(Equipo f, float v)
    {
        bool isUpdated = false;
        if (v > valor)
            {
            valor = v;
            equipo = f;
            isUpdated = true;
            }
        return isUpdated;
    }
}

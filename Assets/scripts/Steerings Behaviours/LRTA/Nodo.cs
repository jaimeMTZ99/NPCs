using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo{

    public int X;//X Position in the Node Array
    public int Y;//Y Position in the Node Array

    public bool bIsWall;        //si hay o no camino
    public Vector3 vPosition;   //posicion

    public Nodo ParentNode;//For the AStar algoritm, will store what node it previously came from so it cn trace the shortest path.

    public int igCost;  //coste nodo siguiente
    public int ihCost;  //coste destino

    public int FCost { get { return igCost + ihCost; } }//Quick get function to add G cost and H Cost, and since we'll never need to edit FCost, we dont need a set function.
    
    public Nodo(bool a, Vector3 b, int c, int d) //Constructor
    {
        bIsWall = a;
        vPosition = b;
        X = c;
        Y = d;
    }

}
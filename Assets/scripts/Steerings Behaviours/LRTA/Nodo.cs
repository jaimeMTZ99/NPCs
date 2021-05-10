using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nodos que componen los caminos del pathfinding y del algoritmo LRTA estrella
public class Nodo{

    public int X;   //Posicion x con respecto a los nodos
    public int Y;   //Posicion y con respecto a los nodos

    public bool aislado;     //Aislamiento del nodo, si es posible continuar
    public Vector3 Posicion;   //posicion

    public Nodo NodoPadre;  //Nodo del que se accede al actual

    public int igCost;  //coste nodo siguiente
    public int ihCost;  //coste destino

    public int FCost { get { return igCost + ihCost; } } //Suma de costes
    
    public Nodo(bool a, Vector3 b, int c, int d) //Constructor
    {
        aislado = a;
        Posicion = b;
        X = c;
        Y = d;
    }

}
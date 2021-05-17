using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nodos que componen los caminos del pathfinding y del algoritmo LRTA estrella
public class Nodo{
    public enum TerrainType {
        Road,
        Grassland,
        Forest,
        RedCapturePoint,
        BluCapturePoint,
        BluBase,
        RedBase,
        Undefined,
        NotWalkable
    }
    public int X;   //Posicion x con respecto a los nodos
    public int Y;   //Posicion y con respecto a los nodos

    public bool walkable;     //Aislamiento del nodo, si es posible continuar
    public Vector3 Posicion;   //posicion

    public Nodo NodoPadre;  //Nodo del que se accede al actual

    public float igCost;  //coste nodo siguiente
    public float ihCost;  //coste destino
    public float FCost { get { return igCost + ihCost; } } //Suma de costes
    
    public TerrainType terrainType;
    public float influence;
    public Nodo(bool a, Vector3 b, int c, int d, TerrainType terreno) //Constructor
    {
        walkable = a;
        Posicion = b;
        X = c;
        Y = d;
        terrainType = terreno;
    }
    public float SpeedMultiplier(NPC.TipoUnidad unitType) {
        switch (terrainType) {
            case TerrainType.Forest:
                switch (unitType) {
                    case NPC.TipoUnidad.Ranged:
                        return 2.5f; 
                    case NPC.TipoUnidad.Brawler: 
                        return 2f;
                    case NPC.TipoUnidad.Medic:
                        return 3;
                }
                break;
            case TerrainType.Grassland:
                switch (unitType) {
                    case NPC.TipoUnidad.Ranged:
                        return 2.75f; 
                    case NPC.TipoUnidad.Brawler:
                        return 2.5f;
                    case NPC.TipoUnidad.Medic:
                        return 2.5f;
                    
                }
                break;
            case TerrainType.Road:
                switch (unitType) {
                    case NPC.TipoUnidad.Ranged:
                        return 2.75f; 
                    case NPC.TipoUnidad.Brawler:
                        return 3;
                    case NPC.TipoUnidad.Medic:
                        return 3;
                    
                }
                break;
            default:
                return 2;
        }
        return 2;
    }

}
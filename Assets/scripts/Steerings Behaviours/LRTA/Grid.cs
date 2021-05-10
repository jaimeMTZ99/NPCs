using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Transform StartPosition;//This is where the program will start the pathfinding from.
    public Vector2 vGridWorldSize;//A vector2 to store the width and height of the graph in world units.
    public float fNodeRadius;//This stores how big each square on the graph will be
    public float fDistanceBetweenNodes;//The distance that the squares will spawn from eachother.

    public Nodo[,] NodeArray;//The array of nodes that the A Star algorithm uses.
    Transform[,] mapa;
    public GameObject cubos;
    public int mapaFila;
    public int mapaColumna;
    private Transform fila;
    private Transform columna;
    public List<Nodo> FinalPath;//The completed path that the red line will be drawn along
    float fNodeDiameter;//Twice the amount of the radius (Set in the start function)
    int iGridSizeX, iGridSizeY;//Size of the Grid in Array units.


    private void Awake()
    {
        mapa = new Transform[mapaFila, mapaColumna];
        for (int i = 0; i < mapaFila; i++)
        {
            fila = cubos.transform.GetChild(i);
            for (int y = 0; y < mapaColumna; y++)
            {
                columna = fila.transform.GetChild(y);
                mapa[y, i] = columna;
            }
        }
        
       
    }


    private void Start()
    {
        fNodeDiameter = fNodeRadius * 2;
        iGridSizeX = Mathf.RoundToInt(vGridWorldSize.x / fNodeDiameter);
        iGridSizeY = Mathf.RoundToInt(vGridWorldSize.y / fNodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        int[,] matrizCostes = ObtenerMatrizCostes();
        NodeArray = new Nodo[iGridSizeX, iGridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * vGridWorldSize.x / 2 - Vector3.forward * vGridWorldSize.y / 2;
        for (int x = 0; x < iGridSizeX; x++)
        {
            for (int y = 0; y < iGridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * fNodeDiameter + fNodeRadius) + Vector3.forward * (y * fNodeDiameter + fNodeRadius);
                bool Wall = true;
                NodeArray[x, y] = new Nodo(Wall, worldPoint, x, y);//Create a new node in the array.
                NodeArray[x,y].igCost = matrizCostes[x,y];
            }
        }
    }

    //Function that gets the neighboring nodes of the given node.
    public List<Nodo> GetNeighboringNodes(Nodo z)
    {
        List<Nodo> NeighborList = new List<Nodo>();
        int icheckX;
        int icheckY;

        icheckX = z.X + 1;
        icheckY = z.Y;
        if (icheckX >= 0 && icheckX < iGridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }
        //Check the Left side of the current node.
        icheckX = z.X - 1;
        icheckY = z.Y;
        if (icheckX >= 0 && icheckX < iGridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }
        //Check the Top side of the current node.
        icheckX = z.X;
        icheckY = z.Y + 1;
        if (icheckX >= 0 && icheckX < iGridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }
        //Check the Bottom side of the current node.
        icheckX = z.X;
        icheckY = z.Y - 1;
        if (icheckX >= 0 && icheckX < iGridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }

        return NeighborList;
    }


    //Gets the closest node to the given world position.
    public Nodo NodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        float ixPos = ((a_vWorldPos.x + vGridWorldSize.x / 2) / vGridWorldSize.x);
        float iyPos = ((a_vWorldPos.z + vGridWorldSize.y / 2) / vGridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((iGridSizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((iGridSizeY - 1) * iyPos);
        if (NodeArray != null && ix < mapaFila && ix >= 0 &&  iy < mapaColumna && iy >= 0)
            return NodeArray[ix, iy];
        return null;
    }

    int costeNodo(Transform nodo)
    {
        if(isObjectHere(nodo.position)){
            return 99999;
        }
        return 1;
    }

    public int[,] ObtenerMatrizCostes()
    {
        int[,] mapaCostes = new int[mapaFila, mapaColumna];
        for (int i = 0; i < mapaFila; i++)
        {
            for (int y = 0; y < mapaColumna; y++)
            {
                mapaCostes[i, y] = costeNodo(mapa[i, y]);
            }
        }
        return mapaCostes;
    }
    
    bool isObjectHere(Vector3 position)
    {
        Collider[] intersecting = Physics.OverlapSphere(position, fNodeRadius);
        foreach (Collider i in intersecting){
            if(i.gameObject.tag == "Muro"){

                return true;
            }
        }
        return false;
    }
}
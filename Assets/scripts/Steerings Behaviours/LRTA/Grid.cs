using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Transform StartPosition;//This is where the program will start the pathfinding from.
    public LayerMask WallMask;//This is the mask that the program will look for when trying to find obstructions to the path.
    public Vector2 vGridWorldSize;//A vector2 to store the width and height of the graph in world units.
    public float fNodeRadius;//This stores how big each square on the graph will be
    public float fDistanceBetweenNodes;//The distance that the squares will spawn from eachother.

    Nodo[,] NodeArray;//The array of nodes that the A Star algorithm uses.
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


    private void Start()//Ran once the program starts
    {
        fNodeDiameter = fNodeRadius * 2;//Double the radius to get diameter
        iGridSizeX = Mathf.RoundToInt(vGridWorldSize.x / fNodeDiameter);//Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
        iGridSizeY = Mathf.RoundToInt(vGridWorldSize.y / fNodeDiameter);//Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
        CreateGrid();//Draw the grid
    }

    void CreateGrid()
    {
        int[,] matrizCostes = ObtenerMatrizCostes();
        NodeArray = new Nodo[iGridSizeX, iGridSizeY];//Declare the array of nodes.
        Vector3 bottomLeft = transform.position - Vector3.right * vGridWorldSize.x / 2 - Vector3.forward * vGridWorldSize.y / 2;//Get the real world position of the bottom left of the grid.
        for (int x = 0; x < iGridSizeX; x++)//Loop through the array of nodes.
        {
            for (int y = 0; y < iGridSizeY; y++)//Loop through the array of nodes
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * fNodeDiameter + fNodeRadius) + Vector3.forward * (y * fNodeDiameter + fNodeRadius);//Get the world co ordinates of the bottom left of the graph
                bool Wall = true;//Make the node a wall

                //If the node is not being obstructed
                //Quick collision check against the current node and anything in the world at its position. If it is colliding with an object with a WallMask,
                //The if statement will return false.
                if (Physics.CheckSphere(worldPoint, fNodeRadius, WallMask))
                {
                    Wall = false;//Object is not a wall
                }
                NodeArray[x, y] = new Nodo(Wall, worldPoint, x, y);//Create a new node in the array.
                NodeArray[x,y].igCost = matrizCostes[x,y];
                if (NodeArray[x,y].igCost == 99999)
                    print("Nodo: "+ NodeArray[x,y].vPosition  + " Coste" + NodeArray[x,y].igCost);
            }
        }
    }

    //Function that gets the neighboring nodes of the given node.
    public List<Nodo> GetNeighboringNodes(Nodo a_NeighborNode)
    {
        List<Nodo> NeighborList = new List<Nodo>();//Make a new list of all available neighbors.
        int icheckX;//Variable to check if the XPosition is within range of the node array to avoid out of range errors.
        int icheckY;//Variable to check if the YPosition is within range of the node array to avoid out of range errors.

        //Check the right side of the current node.
        icheckX = a_NeighborNode.iGridX + 1;
        icheckY = a_NeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < iGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Left side of the current node.
        icheckX = a_NeighborNode.iGridX - 1;
        icheckY = a_NeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < iGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Top side of the current node.
        icheckX = a_NeighborNode.iGridX;
        icheckY = a_NeighborNode.iGridY + 1;
        if (icheckX >= 0 && icheckX < iGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Bottom side of the current node.
        icheckX = a_NeighborNode.iGridX;
        icheckY = a_NeighborNode.iGridY - 1;
        if (icheckX >= 0 && icheckX < iGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }

        return NeighborList;//Return the neighbors list.
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
        return NodeArray[ix, iy];
    }

    int costeNodo(Transform nodo)
    {
        if(isObjectHere(nodo.position)){
            //Debug.Log("Nodo en la posicion " + nodo.position + " es un muro");
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
                Debug.Log(mapaCostes[i,y]);
            }
        }
        return mapaCostes;
    }
    

    public string getTagNodo(int x, int y)
    {
        if (mapa[x, y].tag == null) Debug.Log(x + " " + y);
        return mapa[x, y].tag;
    }
    bool isObjectHere(Vector3 position)
    {
        Collider[] intersecting = Physics.OverlapSphere(position, 1f);
        foreach (Collider i in intersecting){
            if(i.gameObject.tag == "Muro"){

                return true;
            }
        }
        return false;
    }
}
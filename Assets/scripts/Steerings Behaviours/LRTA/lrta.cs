using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrta : MonoBehaviour
{


    public class Nodo{

        public int x;
        public int y;
        public bool wall;
        public Vector3 pos;
        public Nodo vecino;

        public int costeNodo;
        public int costeDestino;

        public int costeTotal { 
            get { 
                return costeNodo + costeDestino; 
                } 
            }
    
        public Nodo(bool w, Vector3 p, int px, int py)
        {
            wall = w;
            pos = p;
            x = px;
            y = py;
        }

    }


    [SerializeField]
    private List<Vector3> grid;


    public List<Nodo> FindPath(Nodo comienzo, Nodo final, int distancia, float [,] mapaCostes)
    {
        
        List<Nodo> ClosedList = new List<Nodo>();
        
        Nodo actualNode = startNode;
        int moveCost;
        switch (distance)
        {
            case 1:
                moveCost = actualNode.igCost + Manhattan(actualNode, targetNode);
                break;
            case 2:
                moveCost = actualNode.igCost + Chebychev(actualNode, targetNode);
                break;
            case 3:
                moveCost = actualNode.igCost + Euclidea(actualNode, targetNode);
                break;
            default:
                moveCost = actualNode.igCost + Manhattan(actualNode, targetNode);
                break;
        }
        actualNode.ihCost = moveCost;
        //Mientras el estado actual no sea el estado objetivo
        while (actualNode != targetNode)
        {
            Nodo nextNode = null;
            List<Nodo> nodesVecinos = grid.GetNeighboringNodes(actualNode);
           
            foreach (Nodo NeighborNode in nodesVecinos)
            {
                moveCost = 0;
                    switch (distance)
                    {
                        case 1:
                            moveCost = actualNode.igCost + Manhattan(NeighborNode, targetNode);
                            break;
                        case 2:
                            moveCost = actualNode.igCost + Chebychev(NeighborNode, targetNode);
                            break;
                        case 3:
                            moveCost = actualNode.igCost + Euclidea(NeighborNode, targetNode);
                            break;
                    }
                    NeighborNode.igCost = moveCost; 
                
            }
            
            float minCost = Mathf.Infinity;
            foreach (Nodo NeighborNode in nodesVecinos)
            {
                if (NeighborNode.bIsWall)
                {
                    if (minCost == Mathf.Infinity)
                    {
                        nextNode = NeighborNode;
                        minCost = NeighborNode.FCost;
                    }
                    else if (NeighborNode.FCost < nextNode.FCost)
                    {
                        nextNode = NeighborNode;
                        minCost = NeighborNode.FCost;
                    }
                }
            }
            ClosedList.Add(actualNode);
            actualNode = nextNode;
        }
        return ClosedList;
    }

    List<Nodo> GetFinalPath(Nodo a_StartingNode, Nodo a_EndNode, MyGrid grid)
    {
        List<Nodo> FinalPath = new List<Nodo>();//List to hold the path sequentially 
        Nodo CurrentNode = a_EndNode;//Node to store the current node being checked

        while (CurrentNode != a_StartingNode)//While loop to work through each node going through the parents to the beginning of the path
        {
            FinalPath.Add(CurrentNode);//Add that node to the final path
            CurrentNode = CurrentNode.ParentNode;//Move onto its parent node
        }

        FinalPath.Reverse();//Reverse the path to get the correct order

        grid.FinalPath = FinalPath;//Set the final path

        return FinalPath;

    }


    //heuristicas
    int Manhattan(Nodo a, Nodo b)
    {
        int x = Mathf.Abs(a.x - b.x);
        int y = Mathf.Abs(a.y - b.y);
        return x + y;
    }

    
    int Chebychev(Nodo a, Nodo b)
    {
        int x = Mathf.Abs(b.x - a.x);
        int y = Mathf.Abs(b.y - a.y);

        return Math.Max(x, y);
    }

    
    int Euclidea(Nodo a, Nodo b)
    {
        int x = (b.x - a.x) * (b.x - a.x);
        int y = (b.y - a.y) * (b.y - a.y);

        return (int) Mathf.Sqrt(x+y);
    }








}

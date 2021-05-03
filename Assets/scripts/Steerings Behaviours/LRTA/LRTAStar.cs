using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRTAStar : MonoBehaviour
{
    public List<Nodo> FindPath(Nodo startNode, Nodo targetNode, int distance, Grid grid)
    {
        
        List<Nodo> ClosedList = new List<Nodo>();
        
        Nodo actualNode = startNode;
        int moveCost;
        switch (distance)
        {
            case 1:
                moveCost = GetManhattenDistance(actualNode, targetNode);
                break;
            case 2:
                moveCost = GetChebyshevDistance(actualNode, targetNode);
                break;
            case 3:
                moveCost = GetEuclideDistance(actualNode, targetNode);
                break;
            default:
                moveCost = GetManhattenDistance(actualNode, targetNode);
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
                            moveCost = GetManhattenDistance(NeighborNode, targetNode);
                            break;
                        case 2:
                            moveCost = GetChebyshevDistance(NeighborNode, targetNode);
                            break;
                        case 3:
                            moveCost = GetEuclideDistance(NeighborNode, targetNode);
                            break;
                    }
                    NeighborNode.ihCost = moveCost; 
                
            }
            
            float minCost = Mathf.Infinity;
            foreach (Nodo NeighborNode in nodesVecinos)
            {
                if (NeighborNode.bIsWall && !ClosedList.Contains(NeighborNode))
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
    List<Nodo> GetFinalPath(Nodo a_StartingNode, Nodo a_EndNode, Grid grid)
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

    //Calculo de distancias segun tres Heuristicas:

    // Manhattan
    int GetManhattenDistance(Nodo a_nodeA, Nodo a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);//x1-x2
        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);//y1-y2

        return ix + iy;//Return the sum
    }

    // Chebyshev
    int GetChebyshevDistance(Nodo a_nodeA, Nodo a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeB.iGridX - a_nodeA.iGridX);
        int iy = Mathf.Abs(a_nodeB.iGridY - a_nodeA.iGridY);

        return Mathf.Max(ix, iy);
    }

    // Euclidea
    int GetEuclideDistance(Nodo a_nodeA, Nodo a_nodeB)
    {
        int ix = (a_nodeB.iGridX - a_nodeA.iGridX) * (a_nodeB.iGridX - a_nodeA.iGridX);
        int iy = (a_nodeB.iGridY - a_nodeA.iGridY) * (a_nodeB.iGridY - a_nodeA.iGridY);

        return (int) Mathf.Sqrt(ix+iy);
    } 
}

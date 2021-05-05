using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRTAStar : MonoBehaviour
{
    public List<Nodo> FindPath(Nodo startNode, Nodo targetNode, int distance, Grid grid)
    {
        if (grid.NodeArray == null)
            return null;
        List<Nodo> ClosedList = new List<Nodo>();
        Nodo actualNode = startNode;
        int moveCost;
        switch (distance)
        {
            case 1:
                moveCost = Manhattan(actualNode, targetNode);
                break;
            case 2:
                moveCost = Chebychev(actualNode, targetNode);
                break;
            case 3:
                moveCost = Euclide(actualNode, targetNode);
                break;
            default:
                moveCost = Manhattan(actualNode, targetNode);
                break;
        }
        actualNode.ihCost = moveCost;
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
                            moveCost = Manhattan(NeighborNode, targetNode);
                            break;
                        case 2:
                            moveCost = Chebychev(NeighborNode, targetNode);
                            break;
                        case 3:
                            moveCost = Euclide(NeighborNode, targetNode);
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
    List<Nodo> GetFinalPath(Nodo a, Nodo b, Grid grid)
    {
        List<Nodo> FinalPath = new List<Nodo>();
        Nodo CurrentNode = b;
        while (CurrentNode != a)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.ParentNode;
        }

        FinalPath.Reverse();
        grid.FinalPath = FinalPath;
        return FinalPath;
    }

    int Manhattan(Nodo a, Nodo b)
    {
        int ix = Mathf.Abs(a.X - b.X);
        int iy = Mathf.Abs(a.Y - b.Y);
        return ix + iy;
    }

    int Chebychev(Nodo a, Nodo b)
    {
        int ix = Mathf.Abs(b.X - a.X);
        int iy = Mathf.Abs(b.Y - a.Y);
        return Mathf.Max(ix, iy);
    }

    int Euclide(Nodo a, Nodo b)
    {
        int ix = (b.X - a.X) * (b.X - a.X);
        int iy = (b.Y - a.Y) * (b.Y - a.Y);
        return (int) Mathf.Sqrt(ix+iy);
    } 
}

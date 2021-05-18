using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LRTA : MonoBehaviour
{
    public List<Nodo> EncontrarCaminoLRTAStar(Nodo comienzo, Nodo objetivo, int distancia, Grid grid)
    {
        //Lista de nodos cerrados a.k.a nodos utilizados
        if (grid.Nodos == null)
            return null;
        List<Nodo> cerrados = new List<Nodo>();
        Nodo actual = comienzo;
        int coste;
        //Aplicamos la heuristica especificada entre el nodo actual (inicial) y el destino
        switch (distancia)
        {
            case 1:
                coste = Manhattan(actual, objetivo);
                break;
            case 2:
                coste = Chebychev(actual, objetivo);
                break;
            case 3:
                coste = Euclide(actual, objetivo);
                break;
            default:
                coste = Manhattan(actual, objetivo);
                break;
        }

        //Establecemos el coste del camino
        actual.ihCost = coste;

        //Mientras que no se haya alcanzado el nodo objetivo
        while (actual != objetivo)
        {
            Nodo siguiente = null;
            List<Nodo> vecinos = grid.GetVecinos(actual);
            //Buscamos de entre sus nodos vecinos
            foreach (Nodo vecino in vecinos)
            {
                coste = 0;
                //Para cada vecino, calcula su coste hasta el objetivo
                switch (distancia)
                {
                    case 1:
                        coste = Manhattan(vecino, objetivo);
                        break;
                    case 2:
                        coste = Chebychev(vecino, objetivo);
                        break;
                    case 3:
                        coste = Euclide(vecino, objetivo);
                        break;
                }
                vecino.ihCost = coste;

            }
            float costeMinimo = Mathf.Infinity;

            //Para cada vecino
            foreach (Nodo vecino in vecinos)
            {
                //Si no está en la lista de cerrados y no está aislado
                //establece el vecino como el siguiente nodo del camino
                //obteniendo el nodo cuyo coste es menor
                if (!cerrados.Contains(vecino))
                {
                    if (costeMinimo == Mathf.Infinity)
                    {
                        siguiente = vecino;
                        costeMinimo = vecino.FCost;
                    }
                    else if (vecino.FCost < siguiente.FCost)
                    {
                        siguiente = vecino;
                        costeMinimo = vecino.FCost;
                    }
                }
            }
            //Añade el nodo a la lista de cerrados
            cerrados.Add(actual);
            //Establecemos el nodo actual como el siguiente nodo del camino
            actual = siguiente;
            if (siguiente == null)
            {
                break;
            }
        }
        //Devolvemos el camino
        return cerrados;
    }

    //Encuentra un camino dado un nodo por el que comenzar, y un destino
    //Distancia se usa para especificar que tipo de heuristica utilizar
    public List<Nodo> EncontrarCaminoAStar(Nodo comienzo, Nodo objetivo, int distancia, Grid grid, NPC npc, bool tactico, float multiplicadorTerreno, float multiplicadorInfluencia, float multiplicadorVisibilidad)
    {
        //En caso de que el grid no contenga nodos, no se hace nada
        if (grid.Nodos == null)
            return null;
        List<Nodo> openSet = new List<Nodo>();
        List<Nodo> closedSet = new List<Nodo>();
        int coste = 0;
        openSet.Add(comienzo);
        while (openSet.Count > 0)
        {
            Nodo currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                switch (distancia)
                {
                    case 1:
                        currentNode.ihCost = Manhattan(currentNode, objetivo);
                        openSet[i].ihCost = Manhattan(openSet[i], objetivo);
                        break;
                    case 2:
                        currentNode.ihCost = Chebychev(currentNode, objetivo);
                        openSet[i].ihCost = Chebychev(openSet[i], objetivo);
                        break;
                    case 3:
                        currentNode.ihCost = Euclide(currentNode, objetivo);
                        openSet[i].ihCost = Euclide(openSet[i], objetivo);
                        break;
                    default:
                        currentNode.ihCost = Manhattan(currentNode, objetivo);
                        openSet[i].ihCost = Manhattan(openSet[i], objetivo);
                        break;
                }
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].ihCost < currentNode.ihCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            if (currentNode == objetivo)
            {
                return RetracePath(comienzo, objetivo);
            }
            List<Nodo> vecinos = grid.GetVecinos(currentNode);
            foreach (Nodo neighbour in vecinos)
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;
                switch (distancia)
                {
                    case 1:
                        coste = Manhattan(currentNode, neighbour);
                        break;
                    case 2:
                        coste = Chebychev(currentNode, neighbour);
                        break;
                    case 3:
                        coste = Euclide(currentNode, neighbour);
                        break;
                    default:
                        coste = Manhattan(currentNode, neighbour);
                        break;
                }
                float newMovementCostToNeighbour = 0;
                if (tactico)
                {
                    newMovementCostToNeighbour = costeVecinoTactico(currentNode, neighbour, multiplicadorTerreno, grid, npc.tipo, npc.team, coste,multiplicadorInfluencia, multiplicadorVisibilidad);
                }
                else
                {
                    newMovementCostToNeighbour = currentNode.igCost + coste;
                }
                if (newMovementCostToNeighbour < neighbour.igCost || !openSet.Contains(neighbour))
                {
                    neighbour.igCost = newMovementCostToNeighbour;
                    switch (distancia)
                    {
                        case 1:
                            neighbour.ihCost = Manhattan(currentNode, neighbour);
                            break;
                        case 2:
                            neighbour.ihCost = Chebychev(currentNode, neighbour);
                            break;
                        case 3:
                            neighbour.ihCost = Euclide(currentNode, neighbour);
                            break;
                        default:
                            neighbour.ihCost = Manhattan(currentNode, neighbour);
                            break;
                    }
                    neighbour.NodoPadre = currentNode;
                    Collider[] hitColliders = Physics.OverlapSphere(neighbour.Posicion, 1);
                    bool hayPersonaje = false;
                    foreach (Collider c in hitColliders){
                         if (c.tag == "PathFindingAStar"){
                             hayPersonaje = true;
                         }
                     }
                    if (!openSet.Contains(neighbour) || !hayPersonaje)
                        openSet.Add(neighbour);
                }
            }
        }
        return RetracePath(comienzo, objetivo);
    }
    float costeVecinoTactico(Nodo currentNode, Nodo vecino, float multiplicadorTerreno,
     Grid grid, NPC.TipoUnidad tipo, NPC.Equipo team, int heuristica, float multiplicadorInfluencia, float multiplicadorVisibilidad)
    {
        float finalCost = 0;
        //Puede ser que haya cmabiarlo a (multiplicadorTerreno * (costeNodoTactico + hCost))
        float terrainCost = multiplicadorTerreno * (grid.costeNodoTactico(currentNode, tipo, team) + grid.costeNodoTactico(vecino, tipo, team)) / 2;
        float currentInfluence;
        float adjacentInfluence;
        if (team == NPC.Equipo.Spain)
        {
            // Ignore friendly influence (positive values)
            // Avoid enemy influence areas (negative values turned positive)
            if (currentNode.influence > 0)
                currentInfluence = 0;
            else
                currentInfluence = Mathf.Abs(currentNode.influence);

            if (vecino.influence > 0)
                adjacentInfluence = 0;
            else
                adjacentInfluence = Mathf.Abs(currentNode.influence);
        }
        else
        {
            // Ignore friendly influence (negative values)
            // Avoid enemy influence areas (positive values)
            if (currentNode.influence < 0)
                currentInfluence = 0;
            else
                currentInfluence = currentNode.influence;

            if (vecino.influence < 0)
                adjacentInfluence = 0;
            else
                adjacentInfluence = vecino.influence;
        }

        float influenceCost = multiplicadorInfluencia * (currentInfluence + adjacentInfluence) / 2;
        float visibilityCost = multiplicadorVisibilidad * (1 / currentNode.costeNodoVisibilidad()) * 10;
        finalCost += terrainCost + influenceCost + visibilityCost;
        return finalCost;
    }
    List<Nodo> RetracePath(Nodo comienzo, Nodo objetivo)
    {
        List<Nodo> path = new List<Nodo>();
        Nodo currentNode = objetivo;
        while (currentNode != comienzo)
        {
            path.Add(currentNode);
            currentNode = currentNode.NodoPadre;
        }
        path.Reverse();
        return path;
    }
    //Calcula la distancia Manhattan entre dos nodos
    int Manhattan(Nodo a, Nodo b)
    {
        int ix = Mathf.Abs(a.X - b.X);
        int iy = Mathf.Abs(a.Y - b.Y);
        return ix + iy;
    }

    //Calcula la distancia Chebychev entre dos nodos
    int Chebychev(Nodo a, Nodo b)
    {
        int ix = Mathf.Abs(b.X - a.X);
        int iy = Mathf.Abs(b.Y - a.Y);
        return Mathf.Max(ix, iy);
    }

    //Calcula la distancia Euclidea entre dos nodos
    int Euclide(Nodo a, Nodo b)
    {
        int ix = (b.X - a.X) * (b.X - a.X);
        int iy = (b.Y - a.Y) * (b.Y - a.Y);
        return (int)Mathf.Sqrt(ix + iy);
    }
}

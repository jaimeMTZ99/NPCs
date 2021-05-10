using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRTA : MonoBehaviour
{
    //Encuentra un camino dado un nodo por el que comenzar, y un destino
    //Distancia se usa para especificar que tipo de heuristica utilizar
    public List<Nodo> EncontrarCamino(Nodo comienzo, Nodo objetivo, int distancia, Grid grid)
    {
        //En caso de que el grid no contenga nodos, no se hace nada
        if (grid.Nodos == null)
            return null;
        //Lista de nodos cerrados a.k.a nodos utilizados
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
                if (vecino.aislado && !cerrados.Contains(vecino))
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
        }
        //Devolvemos el camino
        return cerrados;
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
        return (int) Mathf.Sqrt(ix+iy);
    } 
}

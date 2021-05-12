using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceMap : Graph
{
    /**
    public List<NPC> unidades;
    GameObject[] posiciones;

    void Awake()
    {
        if (unidades == null)
            unidades = new List<NPC>();
    }

    public void AddUnit(NPC u)
    {
        if (unidades.Contains(u))
            return;
        unidades.Add(u);
    }

    public void RemoveUnit(NPC u)
    {
        unidades.Remove(u);
    }
    public void ComputeInfluenceSimple()
    {
        int vId;
        GameObject vObj;
        Punto v;
        float dropOff;
        List<Vertex> pendientes = new List<Vertex>();
        List<Vertex> visitados = new List<Vertex>();
        List<Vertex> frontera;
        Vertex[] vecinos;
        foreach(NPC u in unidades)
        {
            Vector3 uPos = u.transform.position;
            Vertex vert = GetNearestVertex(uPos);
            pendientes.Add(vert);
            // BFS for assigning influence
            for (int i = 1; i <= u.radio; i++)
            {
                frontera = new List<Vertex>();
                foreach (Vertex p in pendientes)
                {
                    if (visitados.Contains(p))
                        continue;
                    visitados.Add(p);
                    v = p as Punto;
                    dropOff = u.GetDropOff();
                    v.SetValue(u.team, dropOff);

                    vecinos = GetNeighbours(vert);
                    frontera.AddRange(vecinos);
                }
            pendientes = new List<Vertex>(frontera);
            }
        
        }
        
    }**/
}

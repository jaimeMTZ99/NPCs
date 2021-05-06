using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizarGrupos : MonoBehaviour
{
    public Agent[] agentesCombinados;   //lista de agentes con los comportamietnos de flocking

    //las siguientes funciones sirven para indicar quienes van a ser objetivos de los comportamientos de flocking
    private void PutAlignment(Alignment script, int excluded)
    {
        for(int i = 0; i < agentesCombinados.Length; i++)
        {
            if (i != excluded)
                script.Targets.Add(agentesCombinados[i].GetComponent<Agent>());
        }
    }

    private void PutCohesion(Cohesion script, int excluded)
    {
        for (int i = 0; i < agentesCombinados.Length; i++)
        {
            if (i != excluded)
                script.Targets.Add(agentesCombinados[i].GetComponent<Agent>());
        }
    }

    private void PutSeparation(Separation script, int excluded)
    {
        for (int i = 0; i < agentesCombinados.Length; i++)
        {
            if (i != excluded)
                script.Targets.Add(agentesCombinados[i].GetComponent<Agent>());
        }
    }

    void Start()
    {
        int i = 0;
        foreach (Agent a in agentesCombinados)
        {
            agentesCombinados[i++] = a;
        }

        for (i = 0; i < agentesCombinados.Length; i++)
        {
            PutAlignment(agentesCombinados[i].GetComponent<Alignment>(), i);
            PutCohesion(agentesCombinados[i].GetComponent<Cohesion>(), i);
            PutSeparation(agentesCombinados[i].GetComponent<Separation>(), i);
        }
    }
}
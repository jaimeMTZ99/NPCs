using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class assign the targets of the scripts from every object of the group
public class OrganizarGrupos : MonoBehaviour
{

    public Agent[] agentesCombinados;

    // Assing targets to the Alignment script
    private void PutAlignment(Alignment script, int excluded)
    {
        for(int i = 0; i < agentesCombinados.Length; i++)
        {
            if (i != excluded)
                script.Targets.Add(agentesCombinados[i].GetComponent<Agent>());
        }
    }

    // Assing targets to the Cohesion script
    private void PutCohesion(Cohesion script, int excluded)
    {
        for (int i = 0; i < agentesCombinados.Length; i++)
        {
            if (i != excluded)
                script.Targets.Add(agentesCombinados[i].GetComponent<Agent>());
        }
    }

    // Assing targets to the Separation script
    private void PutSeparation(Separation script, int excluded)
    {
        for (int i = 0; i < agentesCombinados.Length; i++)
        {
            if (i != excluded)
                script.Targets.Add(agentesCombinados[i].GetComponent<Agent>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;

        foreach (Agent a in agentesCombinados)
        {
            agentesCombinados[i++] = a;
        }

        // Update Alignment, Cohesion and Separation of every child
        for (i = 0; i < agentesCombinados.Length; i++)
        {
            // Assign all the agentesCombinados to the Alignment script excluding the actual object
            PutAlignment(agentesCombinados[i].GetComponent<Alignment>(), i);

            // Assign all the agentesCombinados to the Cohesion script excluding the actual object
            PutCohesion(agentesCombinados[i].GetComponent<Cohesion>(), i);

            // Assign all the agentesCombinados to the Separation script excluding the actual object
            PutSeparation(agentesCombinados[i].GetComponent<Separation>(), i);
        }
    }
}
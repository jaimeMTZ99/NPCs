using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Evade))]
[RequireComponent(typeof(ArriveAcceleration))]
public class Hide : SteeringBehaviour
{

    ArriveAcceleration arrive;
    Evade evade;

    public List<Agent> listaObstaculos;
    public float seguro = 0.6f;

    override public Steering GetSteering(AgentNPC agent){
        Steering steer = this.gameObject.GetComponent<Steering>();

        float cercano = Mathf.Infinity;
        GameObject hide = new GameObject("Hide");
        AgentNPC mejorEscondite = hide.AddComponent<AgentNPC>();
        foreach (AgentNPC a in listaObstaculos)
        {
            Vector3 escondite = GetHiding(a);

            float dist = Vector3.Distance(escondite, transform.position);

            if (dist < cercano)
            {
                cercano = dist;
                mejorEscondite = a;
            }
        }
        if (cercano == Mathf.Infinity)
        {
            return evade.GetSteering(agent);
        }

        return arrive.GetSteering(mejorEscondite);
    }



    Vector3 GetHiding(Agent o)
        {
            float distAway = o.intRadius + seguro;

            Vector3 dir = o.transform.position - target.transform.position;
            dir.Normalize();

            return o.transform.position + dir * distAway;
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : ArriveAcceleration
{

    private GameObject hide;
    
    public List<Agent> listaObstaculos;
    public float seguro = 0.6f;

    void Start(){
        hide = new GameObject("Hide");
        hide.AddComponent<AgentNPC>();
    }
    override public Steering GetSteering(AgentNPC agent){
        Steering steer = this.gameObject.GetComponent<Steering>();

        float cercano = Mathf.Infinity;
        AgentNPC mejorEscondite = hide.GetComponent<AgentNPC>();
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


        return base.GetSteering(mejorEscondite);
    }



    Vector3 GetHiding(Agent o)
        {
            float distAway = o.intRadius + seguro;

            Vector3 dir = o.transform.position - target.transform.position;
            dir.Normalize();

            return o.transform.position + dir * distAway;
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    
    public float wanderOffset;
    public float wanderRadius;
    public float wanderRate;
    public float wanderOrientation;
    //private Agent invisible;
    
    private GameObject go;
    void Start(){
        go = new GameObject("Wander");
        Agent invisible = go.AddComponent<Agent>() as Agent;
        /*invisible= Instantiate(aux, aux.transform);
        target = invisible;
        invisible.enabled = false;
        target.enabled = false;
        target.transform.position = this.transform.position;*/

    }
    private Vector3 AsVector(float o) {
        return new Vector3(Mathf.Cos(o), 0, Mathf.Sin(o));
    }
    private float RandomBinomial() {
        // With Random.Range() min is exclusive and max inclusive.
        return Random.Range(0.0f, 1.0f) - Random.Range(0.0f, 1.0f);
    }
    override public Steering GetSteering(AgentNPC agent)
    {
        Agent invisible = go.GetComponent(typeof(Agent)) as Agent;
        invisible.transform.position = agent.transform.position;
        target = invisible;

        //Calcular el ángulo random para hacer face
        wanderOrientation += wanderRate * RandomBinomial(); // Random.Range(-1.0f, 1.0f);
        Debug.Log("Random" + wanderOrientation);
        //Orientación futura del agente.
        target.Orientation = wanderOrientation + agent.Orientation;

       // Debug.Log("Orientacion target" + target.Orientation);
        //Debug.Log("Orientacion agen" + agent.Orientation);

        //Colocar target invisible en posición adelantada del agente.
        Vector3 targetPosition = agent.transform.position + wanderOffset * AsVector(agent.Orientation); 
        targetPosition += wanderRadius * AsVector(target.Orientation);
        Debug.Log("Radio" + wanderRadius);
        target.transform.position = targetPosition;

         
        Debug.Log("Posicion agente" + agent.transform.position);

        Steering steer;

        aux = invisible;
        steer = base.GetSteering(agent);
        //Acelerar al agente
        steer.linear = target.transform.position - agent.transform.position;
        steer.linear.Normalize();
        steer.linear *= agent.maxAcceleration;
        return steer;
    }
}
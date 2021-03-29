using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    
    public float wanderOffset;
    public float wanderRadius;
    public float wanderRate;
    public float wanderOrientation;
    private Agent invisible;
    
    void Start(){
       //v1 = new Vector3(agent.transform.position.x + 10, agent.transform.position.y, agent.transform.position.z + 10);
       //aux.transform.position = v1;
        invisible= Instantiate(aux, aux.transform);
        target = invisible;
        invisible.enabled = false;
        target.enabled = false;
        target.transform.position = this.transform.position;

    }
    private Vector3 AsVector(float o) {
        return new Vector3(Mathf.Cos(o), 0, Mathf.Sin(o));
    }

    override public Steering GetSteering(AgentNPC agent)
    {
        //invisible.enabled = false;
        wanderOrientation += wanderRate * Random.Range(-1.0f, 1.0f);
        //Debug.Log("Aleatorio: " + alt);
        //this.target = invisible;
        target.Orientation = wanderOrientation + agent.Orientation;
        //Debug.Log("orientación invisible: " + target.Orientation);
        //Debug.Log("orientacion target: " + target.Orientation);
        Vector3 targetPosition = this.target.transform.position + wanderOffset * AsVector(agent.Orientation);   
        targetPosition += wanderRadius * AsVector(agent.Orientation);
        //Debug.Log("position invisible: " + target.transform.position);
        target.transform.position = targetPosition;
        Steering steer;

        steer = base.GetSteering(agent);

        steer.linear = target.transform.position - this.transform.position;
        steer.linear.Normalize();
        steer.linear *= agent.maxAcceleration;
        return steer;
    }
}
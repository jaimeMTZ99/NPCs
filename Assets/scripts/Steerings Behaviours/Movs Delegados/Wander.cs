using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    
    public float wanderOffset;
    public float wanderRadius;
    public float wanderRate;
    public float wanderOrientation;

    private Vector3 AsVector(float o) {
        return new Vector3(Mathf.Cos(o), 0, Mathf.Sin(o));
    }

    override public Steering GetSteering(AgentNPC agent)
    {

        wanderOrientation += wanderRate * Random.Range(-1.0f, 1.0f);

        target.Orientation = wanderOrientation+ agent.Orientation;
        target.transform.position = agent.Position + wanderOffset * AsVector(agent.Orientation);
        target.transform.position += wanderRadius * AsVector(target.Orientation);
        Steering steer;

        steer = base.GetSteering(agent);

        steer.linear = agent.maxAcceleration * AsVector(agent.Orientation);
        return steer;
    }
}
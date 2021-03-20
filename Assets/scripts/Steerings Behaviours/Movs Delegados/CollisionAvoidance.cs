using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SeekAcceleration
{


    [SerializeField]
    private float avoidDistance;
    [SerializeField]
    private float lookAhead;

    public override Steering GetSteering(AgentNPC agent) {
        Steering steering = new Steering();;
        Vector3 rayVector = agent.Velocity.normalized * lookAhead;
        RaycastHit hit;
        if (Physics.Raycast(agent.transform.position, rayVector, out hit, lookAhead))
        {
           agent.transform.position = hit.point + hit.normal * avoidDistance;
           target.transform.position = agent.transform.position;
           steering = base.GetSteering(agent);
        }
        return steering;
    }
}

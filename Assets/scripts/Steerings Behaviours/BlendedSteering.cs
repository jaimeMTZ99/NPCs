using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedSteering : SteeringBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> behaviours;


    public override Steering GetSteering(AgentNPC agent) {
        Steering steer = this.gameObject.GetComponent<Steering>();
        steer.linear = Vector3.zero;
        steer.angular = 0;

        foreach (SteeringBehaviour s in behaviours) {
            Steering m = s.GetSteering(agent);
            
            steer.linear += s.weight * m.linear;

            steer.angular += s.weight * m.angular;

        }
 

        float t= Mathf.Max(steer.linear.magnitude,agent.maxAcceleration);
        steer.linear = steer.linear * t;
        steer.angular = Mathf.Max(steer.angular, agent.maxAngularAcc);

        return steer;
    }
}

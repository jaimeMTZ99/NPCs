using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedSteering : SteeringBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> behaviours;
    private Steering steer;
    public override Steering GetSteering(AgentNPC agent) {
        Vector3 multAux = Vector3.zero;
        steer = new Steering();
        foreach (SteeringBehaviour s in behaviours) {
            Steering m = s.GetSteering(agent);
            multAux = (s.weight * m.linear);
            steer.linear = steer.linear + multAux;
            steer.angular += (s.weight * m.angular);
        }
 
        float t= Mathf.Min(steer.linear.magnitude,agent.maxAcceleration);
        steer.linear = steer.linear * t;
        Debug.Log("Rotation" + steer.angular);
        //steer.angular = Mathf.Min(steer.angular, agent.maxAngularAcc);
        return steer;
    }
}

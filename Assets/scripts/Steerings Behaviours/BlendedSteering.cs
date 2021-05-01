using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedSteering : SteeringBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> behaviours;
    private Steering steer;

    void Start(){
        steer = this.gameObject.GetComponent<Steering>();
    }
    public override Steering GetSteering(AgentNPC agent) {

        
        Vector3 multAux = Vector3.zero;
        float multAng = 0;
        Steering m;
        foreach (SteeringBehaviour s in behaviours) {
            m = s.GetSteering(agent);
            multAux += (s.weight * m.linear);
            multAng += (s.weight * m.angular);
        }
        steer.linear = multAux;
        steer.angular =  multAng;
        float t= Mathf.Min(steer.linear.magnitude,agent.maxAcceleration);
        steer.linear = steer.linear * t;
        steer.angular = Mathf.Min(steer.angular, agent.maxAngularAcc);
        return steer;
    }
}

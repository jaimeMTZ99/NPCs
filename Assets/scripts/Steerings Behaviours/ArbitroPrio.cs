using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbitroPrio : SteeringBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> behaviours = null;
    private Steering steer;
    public override Steering GetSteering(AgentNPC agent) {
        /*Vector3 multAux = Vector3.zero;
        steer = new Steering();
        foreach (SteeringBehaviour s in behaviours) {
        Steering m = s.GetSteering(agent);
        m = buscar
        return m;*/
        return null;
    }
}

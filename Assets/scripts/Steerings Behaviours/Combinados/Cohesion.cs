using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : SeekAcceleration {

    [SerializeField]
    private float thresholdCohesion = 0f;

    [SerializeField]
    // Targets that I want to approach 
    private List<Agent> targets;

    public List<Agent> Targets {
        get => targets;
        set => targets = value;
    }
    private GameObject goCohesion;
    void Start(){
        goCohesion = new GameObject("Cohesion");
        Agent invisible = goCohesion.AddComponent<Agent>() as Agent;
        target = invisible;
    }
    public override Steering GetSteering(AgentNPC agent) {
        Vector3 direction;
        Vector3 centerOfMass = Vector3.zero;
        float distance = 0f;
        int count = 0;
        // Loop through each target and calculate center of mass
        foreach (Agent target in targets) {
            direction = agent.Position - target.Position;
            distance = Mathf.Abs(direction.magnitude);

            if (distance < thresholdCohesion) {
                centerOfMass += target.Position;
                count++;
            }
        }
        if (count == 0) {
            Steering steering = new Steering();
            return steering;
        }
        centerOfMass /= count;
        // The objective is the center of mass. The point I want to approach
        target.transform.position = centerOfMass;
        return base.GetSteering(agent);
    }
}
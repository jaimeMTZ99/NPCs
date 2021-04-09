using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : SteeringBehaviour {
    [SerializeField]
    private float threshold = 0f;

    [SerializeField]
    // Targets I want to get away from
    private List<Agent> targets;

    public List<Agent> Targets {
        get => targets;
        set => targets = value;
    }

    // Hold the constant coefficient of decay for 
    // the inverse square law force
    [SerializeField]
    private float decayCoefficient;

    [SerializeField]
    private float strength;

    public override Steering GetSteering(AgentNPC agent) {
         Steering steering = new Steering();
         Vector3 direction;
        float distance = 0f;
        // Loop through each target
        foreach (Agent target in targets) {

            // Check if the target is close
            direction = agent.Position - target.Position;
            distance = Mathf.Abs(direction.magnitude);

            if (distance < threshold) {

                // Calculate the strength of repulsion
                float desiredStrength = decayCoefficient/(distance*distance);
                strength = Mathf.Min(desiredStrength, agent.maxAcceleration);

                // Add the acceleration
                steering.linear += direction.normalized * strength;
            }
        }

        // We've gone through all targets, return the result
        return steering;
    }
}
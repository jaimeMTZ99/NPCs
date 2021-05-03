using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : SteeringBehaviour {
    [SerializeField]
    private float threshold = 0f;

    [SerializeField]
    private List<Agent> targets;

    public List<Agent> Targets {
        get => targets;
        set => targets = value;
    }

    [SerializeField]
    private float coef = 1f;

    [SerializeField]
    private float fuerza;
    private GameObject goSeparation;  
    void Start(){
        goSeparation = new GameObject("Separation");
        Agent invisible = goSeparation.AddComponent<Agent>() as Agent;
        target = invisible;
    }

    public override Steering GetSteering(AgentNPC agent) {
        Steering steering = new Steering();
        Vector3 direction;
        float distance = 0f;
        foreach (Agent target in targets) {
            direction = agent.Position - target.Position;
            distance = Mathf.Abs(direction.magnitude);

            if (distance < threshold) {
                float f = coef/(distance*distance);
                fuerza = Mathf.Min(f, agent.maxAcceleration);
                steering.linear += direction.normalized * fuerza;
            }
        }
        return steering;
    }
}
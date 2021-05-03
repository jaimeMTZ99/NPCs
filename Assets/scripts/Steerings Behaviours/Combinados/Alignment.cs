using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alignment : Align {
    [SerializeField]
    private float threshold = 0f;

    [SerializeField]
    private List<Agent> targets;

    public List<Agent> Targets {
        get => targets;
        set => targets = value;
    }
    private GameObject goAlignment;
    void Start(){
        goAlignment = new GameObject("Alignment");
        Agent invisible = goAlignment.AddComponent<Agent>() as Agent;
        target = invisible;
    }
    public override Steering GetSteering(AgentNPC agent) {
        Vector3 direction;
        float heading = 0f;
        float distance = 0f;
        int count = 0;

        foreach (Agent target in targets) {
            direction = agent.Position - target.Position;
            distance = Mathf.Abs(direction.magnitude);

            if (distance < threshold) {
                heading += target.Orientation;
                count++;
            }
        }
        if (count > 0) {
            heading /= count;
        }

        target.Orientation = heading;

        return base.GetSteering(agent);
    }
}
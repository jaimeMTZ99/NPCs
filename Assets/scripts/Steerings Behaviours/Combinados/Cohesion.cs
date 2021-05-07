using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : SeekAcceleration {

    [SerializeField]
    private float thresholdCohesion = 0f;

    [SerializeField]
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
        Vector3 centro = Vector3.zero;
        float distance = 0f;
        int count = 0;

        //vamos calculando el centro de masas en funcion de lo cerca que estan los personajes unos de otros y segun se vaya moviendo
        foreach (Agent target in targets) {
            direction = agent.Position - target.Position;
            distance = Mathf.Abs(direction.magnitude);

            if (distance < thresholdCohesion) {
                centro += target.Position;
                count++;
            }
        }
        if (count == 0) {
            Steering steering = new Steering();
            return steering;
        }
        centro /= count;
        target.transform.position = centro;
        return base.GetSteering(agent);
    }
}
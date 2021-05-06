#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidancePredict : SeekAcceleration
{
    [SerializeField]
    public float Radius = 0.5f;
    private GameObject goCollision;
    [SerializeField]
    private List<Agent> targets;

    public void Start(){
        goCollision = new GameObject("CollisionPredict");
        Agent invisible = goCollision.AddComponent<Agent>() as Agent;
        GameObject[] gs =  GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject g in gs)
        {
            targets.Add(g.GetComponent<Agent>());
        }

    }


    public override Steering GetSteering(AgentNPC agent) {
        Steering steering = this.gameObject.GetComponent<Steering>();
        float shortestTime = Mathf.Infinity;

        Agent firstTarget = null;
        float firstMinSeparation = 0;
        Vector3 firstRelativePos = Vector3.zero;
        float firstDistance = 0;
        Vector3 firstRelativeVel = Vector3.zero;

        float relativeSpeed = 0;
        float timeToCollision = 0;
        foreach (Agent a in targets)
        {
            Vector3 relativePos = a.transform.position - agent.transform.position;
            Vector3 relativeVel = a.Velocity - agent.Velocity;
            relativeSpeed = relativeVel.magnitude;
            timeToCollision = Vector3.Dot(relativePos,relativeVel);
            timeToCollision /= (relativeSpeed * relativeSpeed * -1);

            float distancia = relativePos.magnitude;
            float minSeparation = distancia - relativeSpeed * timeToCollision;
            if (minSeparation > 2* Radius){
                continue;
            }
            if(timeToCollision > 0 && timeToCollision< shortestTime){
                shortestTime = timeToCollision;
                firstTarget = a;
                firstMinSeparation = minSeparation;
                firstDistance = distancia;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }

        }

        if(firstTarget == null)
            return steering;

        if (firstMinSeparation <= 0 || firstDistance < 2*Radius){
            firstRelativePos = firstTarget.transform.position;
        } else {
            firstRelativePos = firstRelativePos + firstRelativeVel * shortestTime;
        }

        firstRelativePos.Normalize();
        steering.linear = -firstRelativePos * agent.maxAcceleration;
        steering.linear.y =0;
        return steering;
    }

}

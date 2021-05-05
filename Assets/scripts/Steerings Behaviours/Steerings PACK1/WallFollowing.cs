using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollowing : SeekAcceleration
{
    public GameObject goWF;
    public float predictTime = 0.1f;
    private Vector3 futurePos;
    public float distancia;
    private List<Collider> col;
    void Start(){
        goWF = new GameObject("WallFollowing");
        target = goWF.AddComponent<Agent>() as Agent;
        target.extRadius = 1.5f;
        target.intRadius = 1.5f;
        col = new List<Collider>();
        GameObject[] a = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject b in a)
        {
            Collider c = b.GetComponent<Collider>();
            if (c!=null)
                col.Add(c);
        }
    }

    public override Steering GetSteering(AgentNPC agent){

        futurePos = agent.transform.position+agent.Velocity*predictTime;
        Vector3 point = Vector3.zero;
        float puntoMasCercano = 99999;
        foreach (Collider d in col){
            Vector3 closestPoint = d.ClosestPoint(futurePos);
            if(closestPoint.magnitude < puntoMasCercano){
                puntoMasCercano = closestPoint.magnitude;
                point = closestPoint;
            }
        }



        return base.GetSteering(agent);
    }
}

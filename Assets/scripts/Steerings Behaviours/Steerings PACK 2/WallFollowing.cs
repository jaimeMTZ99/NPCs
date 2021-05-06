using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollowing : SeekAcceleration
{
    public GameObject goWF;
    public float predictTime = 0.1f;
    private Vector3 futurePos;
    public float distancia = 3;
    [SerializeField]
    private List<GameObject> walls;
    private List<Collider> col;

    void Start(){
        goWF = new GameObject("WallFollowing");
        target = goWF.AddComponent<Agent>() as Agent;
        target.extRadius = 1.5f;
        target.intRadius = 1.5f;
        col = new List<Collider>();
        foreach (GameObject b in walls)
        {
            Collider c = b.GetComponent<Collider>();
            if (c!=null)
                col.Add(c);
        }
    }

    public override Steering GetSteering(AgentNPC agent){

        futurePos = agent.transform.position+agent.Velocity*predictTime;
        GameObject pared = null;
        float puntoMasCercano = 99999;
        for (int i = 0; i< col.Count; i++){
            Vector3 closestPoint = col[i].ClosestPoint(futurePos);
            if(closestPoint.magnitude < puntoMasCercano){
                puntoMasCercano = closestPoint.magnitude;
                pared = walls[i];
            }
        }
        Vector3 normale = Vector3.zero;
        Vector3 dir = pared.transform.position - futurePos;
        RaycastHit hit;
        if (Physics.Raycast(futurePos, dir, out hit))
        {
            Debug.Log(hit);
            normale = hit.normal;
        }

        normale = normale + normale.normalized*distancia;
        target.transform.position = normale;
        return base.GetSteering(agent);
    }
}

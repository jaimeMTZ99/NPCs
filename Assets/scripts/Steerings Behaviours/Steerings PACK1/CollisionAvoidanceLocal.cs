using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidanceLocal : SeekAcceleration
{

    public bool m_Started;
    [SerializeField]
    private float avoidDistance = 1;
    private GameObject goCollision;
    [SerializeField]
    private Agent aux;
    public void Start(){
        goCollision = new GameObject("Collision");
        Agent invisible = goCollision.AddComponent<Agent>() as Agent;
        aux = target;
        target = invisible;
        target.transform.position = aux.transform.position;
        target.intRadius = aux.intRadius;
        target.extRadius = aux.extRadius;
    }
    public override Steering GetSteering(AgentNPC agent) {

        Vector3 pos1 = new Vector3(avoidDistance,0,0);
        Vector3 pos2 = new Vector3(-avoidDistance,0,0);
        Vector3 pos3 = new Vector3(0,0,avoidDistance);
        Vector3 pos4 = new Vector3(0,0,-avoidDistance);

        target.transform.position = aux.transform.position;
        Collider[] intersecting = Physics.OverlapBox(transform.position+pos1, transform.localScale);
        foreach (Collider i in intersecting){
            if(i.gameObject.tag == "Muro"){
                Debug.Log("Collision");
                target.transform.position = i.gameObject.transform.position-pos1;
                return base.GetSteering(agent);
            }
        }
        intersecting = Physics.OverlapBox(transform.position+pos2, transform.localScale);
        foreach (Collider i in intersecting){
            if(i.gameObject.tag == "Muro"){
                Debug.Log("Collision");
                target.transform.position = i.gameObject.transform.position-pos2;
                return base.GetSteering(agent);
            }
        }
        intersecting = Physics.OverlapBox(transform.position+pos3, transform.localScale);
        foreach (Collider i in intersecting){
            if(i.gameObject.tag == "Muro"){
                Debug.Log("Collision");
                target.transform.position = i.gameObject.transform.position-pos3;
                return base.GetSteering(agent);
            }
        }
        intersecting = Physics.OverlapBox(transform.position+pos4, transform.localScale);
        foreach (Collider i in intersecting){
            if(i.gameObject.tag == "Muro"){
                Debug.Log("Collision");
                target.transform.position = i.gameObject.transform.position-pos4;
                return base.GetSteering(agent);
            }
        }       


        Steering steering = this.gameObject.GetComponent<Steering>();
        return steering;
    }
    void OnDrawGizmos()
    {
        Vector3 pos1 = new Vector3(avoidDistance,0,0);
        Vector3 pos2 = new Vector3(-avoidDistance,0,0);
        Vector3 pos3 = new Vector3(0,0,avoidDistance);
        Vector3 pos4 = new Vector3(0,0,-avoidDistance);
        Gizmos.color = Color.red;
        if (m_Started)

            Gizmos.DrawWireCube(transform.position+pos1, transform.localScale);
            Gizmos.DrawWireCube(transform.position+pos2, transform.localScale);
            Gizmos.DrawWireCube(transform.position+pos3, transform.localScale);
            Gizmos.DrawWireCube(transform.position+pos4, transform.localScale);
    }
}

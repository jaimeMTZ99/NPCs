#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SeekAcceleration
{


    [SerializeField]
    private float avoidDistance = 1;
    [SerializeField]
    private float frontaLookAhead = 3;
    [SerializeField]    
    private float whiskersLookahead = 3;
    [SerializeField]
    private float whiskersAngle = 15f;
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
        //creamos los bigotes izquierdo, derecho y frontal junto con los raycast
        Vector3 frontalVector = agent.Velocity.normalized * frontaLookAhead;
        Vector3 leftWhiskerVector = Quaternion.Euler(0, -whiskersAngle, 0) * frontalVector;
        Vector3 rightWhiskerVector = Quaternion.Euler(0, whiskersAngle, 0) * frontalVector;
        target.transform.position = aux.transform.position;
        RaycastHit frontalHit, leftWhiskerHit, rightWhiskerHit;
        //Colisión frontal
        if (Physics.Raycast(agent.transform.position, frontalVector, out frontalHit, frontaLookAhead))
        {
            //detectamos colision
           target.transform.position = frontalHit.point + frontalHit.normal * avoidDistance;
            return base.GetSteering(agent);
        }
        // bigote izquierdo
        if (Physics.Raycast(agent.transform.position, leftWhiskerVector, out leftWhiskerHit, frontaLookAhead)) {
            //detectamos colision
            target.transform.position = leftWhiskerHit.point + leftWhiskerHit.normal * avoidDistance;
             return base.GetSteering(agent);
        }
        // bigote derecho
        if (Physics.Raycast(agent.transform.position, rightWhiskerVector, out rightWhiskerHit, frontaLookAhead)) {
            //detectamos colision
            target.transform.position = rightWhiskerHit.point + rightWhiskerHit.normal * avoidDistance;
             return base.GetSteering(agent);
        }
        Steering steering = this.gameObject.GetComponent<Steering>();
        return steering;
    }
}

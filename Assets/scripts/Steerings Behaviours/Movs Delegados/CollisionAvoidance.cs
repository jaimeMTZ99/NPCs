using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SeekAcceleration
{


    [SerializeField]
    private float avoidDistance;
    [SerializeField]
    private float frontaLookAhead;
    [SerializeField]    
    private float whiskersLookahead;
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
        Steering steering = new Steering();;
        Vector3 frontalVector = agent.Velocity.normalized * frontaLookAhead;
        Vector3 leftWhiskerVector = Quaternion.Euler(0, -whiskersAngle, 0) * frontalVector;
        Vector3 rightWhiskerVector = Quaternion.Euler(0, whiskersAngle, 0) * frontalVector;

        RaycastHit frontalHit, leftWhiskerHit, rightWhiskerHit;
        //Colisión frontal
        if (Physics.Raycast(agent.transform.position, frontalVector, out frontalHit, frontaLookAhead))
        {
           target.transform.position = frontalHit.point + frontalHit.normal * avoidDistance;
        }
        // No frontal collision, check the left whisker
        if (Physics.Raycast(agent.transform.position, leftWhiskerVector, out leftWhiskerHit, frontaLookAhead)) {
            // We detected a left side collision
            target.transform.position = leftWhiskerHit.point + leftWhiskerHit.normal * avoidDistance;
        }
        // No left side collision, check the right whisker
        if (Physics.Raycast(agent.transform.position, rightWhiskerVector, out rightWhiskerHit, frontaLookAhead)) {
            // We detected a right side collision
            target.transform.position = rightWhiskerHit.point + rightWhiskerHit.normal * avoidDistance;
        }
        return base.GetSteering(agent);
    }
}

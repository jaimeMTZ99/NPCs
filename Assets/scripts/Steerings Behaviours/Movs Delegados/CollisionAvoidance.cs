#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SeekAcceleration
{


    [SerializeField]
    private float distancia = 1;
    [SerializeField]
    private float frontal = 3;

    [SerializeField]
    private float angulo = 15f;
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
        Vector3 frontalBigote = agent.Velocity.normalized * frontal;
        Vector3 izqBigote = Quaternion.Euler(0, -angulo, 0) * frontalBigote;
        Vector3 derBigote = Quaternion.Euler(0, angulo, 0) * frontalBigote;
        target.transform.position = aux.transform.position;
        RaycastHit frontalHit, izqHit, derHit;
        //Colisión frontal
        if (Physics.Raycast(agent.transform.position, frontalBigote, out frontalHit, frontal))
        {
            //detectamos colision
           target.transform.position = frontalHit.point + frontalHit.normal * distancia;
            return base.GetSteering(agent);
        }
        // bigote izquierdo
        if (Physics.Raycast(agent.transform.position, izqBigote, out izqHit, frontal)) {
            //detectamos colision
            target.transform.position = izqHit.point + izqHit.normal * distancia;
             return base.GetSteering(agent);
        }
        // bigote derecho
        if (Physics.Raycast(agent.transform.position, derBigote, out derHit, frontal)) {
            //detectamos colision
            target.transform.position = derHit.point + derHit.normal * distancia;
             return base.GetSteering(agent);
        }
        Steering steering = this.gameObject.GetComponent<Steering>();
        return steering;
    }
}

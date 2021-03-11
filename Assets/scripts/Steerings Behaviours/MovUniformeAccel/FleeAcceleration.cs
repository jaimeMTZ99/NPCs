﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeAcceleration : SteeringBehaviour
{

    override public Steering GetSteering(AgentNPC agent)
    {
        //establecer a valores nulos el steering que se debe retornar,
        Steering steer = this.gameObject.GetComponent<Steering>();
        //calculamos la distancia entre objetivo y el agente player (de un punto a otro)
        float distancia = Mathf.Sqrt(Mathf.Pow((target.transform.position.x - this.transform.position.x),2) + 
        0 +
        Mathf.Pow((target.transform.position.z - this.transform.position.z),2));
        //Si la distancia es mayor que el radio interior del target estable la
        //magnitud vectorial del steering como el vector cuya magnitud es la
        //velocidad máxima del agente y cuya dirección va del agente hacia el
        //target
        if(distancia < target.intRadius){
            steer.linear =target.directionToTarget(agent.transform.position) ;
            steer.linear.Normalize();
            steer.linear *=agent.maxAcceleration;
            steer.angular = 0;
        }
        else{
            steer.linear = Vector3.zero;
        }
        return steer;
    }
}

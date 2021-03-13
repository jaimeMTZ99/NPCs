﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAcceleration : SteeringBehaviour
{

    override public Steering GetSteering(AgentNPC agent)
    {
        //establecer a valores nulos el steering que se debe retornar,
        Steering steer = this.gameObject.GetComponent<Steering>();
        //calculamos la distancia entre objetivo y el agente player (de un punto a otro)
        float distancia = Mathf.Sqrt(Mathf.Pow((target.transform.position.x - this.transform.position.x),2) + 
        0 +
        Mathf.Pow((target.transform.position.z - this.transform.position.z),2));
        //Calculamos la direccion del agente al target
        steer.linear = agent.directionToTarget(target.transform.position) ;
        //Si la distancia es menor que el radio interior, entonces ponemos la velocidad en sentido contrario para contrarrestar el mov uniforme
        if(distancia < target.intRadius){
            steer.linear = -agent.Velocity;
        }
        
        steer.linear.Normalize();
        steer.linear *=agent.maxAcceleration;
        steer.angular = 0;
        return steer;

       

    }
}

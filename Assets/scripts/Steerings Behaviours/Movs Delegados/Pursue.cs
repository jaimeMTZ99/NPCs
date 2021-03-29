﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : SeekAcceleration
{

    public float maxPredict;
    public Agent aux = null;

    private Agent invisible; 
    void Start(){
       invisible=  Instantiate(aux, aux.transform);
    }
    public override Steering GetSteering(AgentNPC agent) {
        // Calculamos la distancia y la direccion hacia el objetivo
        Vector3 direction = aux.transform.position - agent.transform.position;
        invisible.enabled = false;
        float distancia = Mathf.Sqrt(Mathf.Pow(aux.transform.position.x - this.transform.position.x,2) + 
        0 +
        Mathf.Pow(aux.transform.position.z - this.transform.position.z,2));
        
        // Obtenemos la velocidad que lleva
        float speed = agent.Velocity.magnitude;
        
        // Comprobamos la velocidad en funcion de la prediccion que hemos hecho
        
        float prediction;
        
        if (speed <= (distancia / maxPredict)) {
            prediction = maxPredict;
        }
         // Calculamos la predcicion si falla
        else {
            prediction = distancia / speed;
        }
        
        // Put the target together
        this.target = invisible;
        invisible.transform.position = aux.transform.position;
        invisible.transform.position += aux.Velocity * prediction;
        
    
        //agent.transform.position += aux.Velocity * prediction;
        
        // Delegate to seek
        return base.GetSteering(agent);
        
    }
}

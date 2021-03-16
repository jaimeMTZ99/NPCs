using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : SeekAcceleration
{

    public float maxPredict;
    private Agent aux;
    public override Steering GetSteering(AgentNPC agent) {

        // Calculamos la distancia y la direccion hacia el objetivo
        Vector3 direction = target.transform.position - agent.transform.position;

        float distancia = Mathf.Sqrt(Mathf.Pow((target.transform.position.x - this.transform.position.x),2) + 
        0 +
        Mathf.Pow((target.transform.position.z - this.transform.position.z),2));

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
        target.transform.position = aux.transform.position;
        target.transform.position += aux.Velocity * prediction;
        
        // Delegate to seek
        return base.GetSteering(agent);
        
    }
}

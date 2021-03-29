using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align {

    public Agent aux;

    public override Steering GetSteering(AgentNPC agent) {
        //Establecemos un steer que sera completamente sin resultados para devolverlo en caso de que la distancia sea 0
        Steering steer= this.gameObject.GetComponent<Steering>(); 
        steer.linear = Vector3.zero;
        steer.angular = 0;
        // Sacamos la direccion y la distancia
        float distancia = Mathf.Sqrt(Mathf.Pow((aux.transform.position.x - this.transform.position.x),2) + 
        0 +
        Mathf.Pow((aux.transform.position.z - this.transform.position.z),2));
        Vector3 direction = aux.transform.position - agent.transform.position;

        // Si la distancia es 0 devolvemos lo que tenemos del steering "vacio"
        if (distancia == 0) {
            return steer;
        }
        //Establecemos como objetivo el agent que nos interesa y cambiamos la orientacion del agente
        this.target = aux;
        targetRotation = Mathf.Atan2(direction.x, direction.z);
        // Devolvemos el control al align
        return base.GetSteering(agent);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveAcceleration : SteeringBehaviour
{
    public float TimeToTarget = 0.1f;
    override public Steering GetSteering(AgentNPC agent)
    {
        //establecer a valores nulos el steering que se debe retornar,
        Steering steer = this.gameObject.GetComponent<Steering>();
        steer.angular = 0;   
        //calculamos la distancia entre objetivo y el agente player (de un punto a otro)
        //float distancia = Mathf.Sqrt(Mathf.Pow((target.transform.position.x - this.transform.position.x),2) + 0 + Mathf.Pow((target.transform.position.z - this.transform.position.z),2));

        Vector3 direction = agent.directionToTarget(target.transform.position);
        Debug.Log(direction);
        float distancia = direction.magnitude;
        //Si la distancia es mayor que el radio interior del target estable la
        //magnitud vectorial del steering como el vector cuya magnitud es la
        //velocidad máxima del agente y cuya dirección va del agente hacia el
        //target

        float targetSpeed;
        if(distancia <= agent.intRadius){
                Debug.Log("Parando");
            steer.linear = -agent.velocity;
            if (steer.linear.magnitude > 0) {
                steer.linear.Normalize();
                steer.linear *= agent.maxAcceleration;
            }
            return steer;
        }

        if(distancia > agent.extRadius)        //si la distancia es mayor que el radio exterior del objetivo
        {           
            Debug.Log("Acelerando");
            targetSpeed = agent.maxSpeed;       //velocidad maxima
        }
        else { 
            Debug.Log("Reducir");
            targetSpeed = agent.maxSpeed* distancia/ agent.extRadius;   //reducimos la velocidad si esta dentro 
        }
        Vector3 targetVelocity;
        targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        steer.linear = targetVelocity-agent.velocity;  //acelearcion intenta llegar a la velocidad del target
        steer.linear /= TimeToTarget;

        if (steer.linear.magnitude > agent.maxAcceleration)  //si esta lejos todavia sigues haciendo "arrive"
        {   Debug.Log("Hola");
            steer.linear.Normalize();
            steer.linear *= agent.maxAcceleration;
        }

    return steer;       //devolver el steering
    }

}
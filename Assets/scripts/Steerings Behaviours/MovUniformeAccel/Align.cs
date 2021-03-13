using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : SteeringBehaviour
{
    [Header("Align")]
    [SerializeField]
    private float _timeToTarget = 0.1f;
    override public Steering GetSteering(AgentNPC agent){
        Steering steer = this.gameObject.GetComponent<Steering>(); 
        steer.linear = Vector3.zero;
        //Rotación deseada.
        float targetRotation;
        // Restamos las orientaciones para calcular el angulo de rotación hacía el target.
        float rotation = this.target.Orientation - agent.Orientation;

        Debug.Log("Rotation:" + rotation);

        // Map the result to the (-pi, pi) interval
        rotation = -Mathf.PI + Mathf.Repeat(rotation + Mathf.PI, 2*Mathf.PI);

        float rotationSize = Mathf.Abs(rotation);
        //Si el agente ya esta rotado en la misma direccion del target paramos.
        if (rotationSize <= agent.intAngle) {
            // Return "none"
            steer.angular = -agent.Rotation;
            if (steer.angular > 0) {
                steer.angular /= Mathf.Abs(steer.angular);
                steer.angular *= agent.MaxAngularAcc;
            }
            return steer;
        }
        //Si el el radio exterior del NPC no está rotado todavía hacia el agentPlayer giramos a maxima velocidad de rotación.
        if (rotationSize > agent.extAngle) {
            targetRotation = agent.MaxRotation;
        }
        else {
            // Si el ángulo exterior ya está alineado con el del agentPlayer se calcula una velocidad de rotación más pequeña para que el ángulo interior también esté alineado.
            targetRotation = agent.MaxRotation * rotationSize / agent.extAngle;
        }

        //Multiplica por +/- 1, es decir, cambia la dirección de rotación deseada.Ya que rotation size es el abs(rotation)
        targetRotation *= rotation / rotationSize;

        // Se intenta coger la rotación deseada.
        steer.angular = targetRotation - agent.Rotation;
        steer.angular /= _timeToTarget;

        //Si la acceleración angular es mayor que la permitida se corrige.
        float angularAcceleration = Mathf.Abs(steer.angular);
        if (angularAcceleration >= agent.MaxAngularAcc){
            steer.angular /= angularAcceleration;
            steer.angular *= agent.MaxAngularAcc;
        }
        return steer;
    }
}

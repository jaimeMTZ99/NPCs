using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : SteeringBehaviour
{
    [Header("Align")]
    [SerializeField]
    private float timeToTarget = 0.1f;

    public float MapToRange (float rotation) {
    rotation %=  Mathf.PI * 2;
    if (Mathf.Abs(rotation) >  Mathf.PI) {
        if (rotation < 0.0f)
            rotation += Mathf.PI * 2;
        else
            rotation -=  Mathf.PI * 2;
    }
    return rotation;
    }

    override public Steering GetSteering(AgentNPC agent){
        Steering steer = this.gameObject.GetComponent<Steering>(); 
        steer.linear = Vector3.zero;
        //Rotación deseada.
        float targetRotation;
        // Restamos las orientaciones para calcular el angulo de rotación hacía el target.
        float rotation = this.target.Orientation - agent.Orientation;
        // Map the result to the (-pi, pi) interval
        rotation = MapToRange(rotation) ;
        float rotationSize = Mathf.Abs(rotation);
        //Si el agente ya esta rotado en la misma direccion del target paramos.
        if (rotationSize <= agent.intAngle) {
            // Return "none"
            Debug.Log("Entrar con rotacion menos que el angulo " + rotationSize);
            steer.angular = -agent.Rotation;
            if (steer.angular >  0){
                steer.angular *= agent.MaxAngularAcc;
            }
            /*if (steer.angular > 0.0f) {
                steer.angular -= agent.MaxAngularAcc;
                if (steer.angular < 0)
                    steer.angular = 0;
            }else if (steer.angular < 0.0f){
                steer.angular += agent.MaxAngularAcc;
                if (steer.angular > 0)
                    steer.angular = 0;
            }*/
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
        steer.angular /= timeToTarget;
        //Si la acceleración angular es mayor que la permitida se corrige.
        float angularAcceleration = Mathf.Abs(steer.angular);
        if (angularAcceleration > agent.MaxAngularAcc){
            steer.angular /= angularAcceleration;
            steer.angular *= agent.MaxAngularAcc;
        }
        Debug.Log("ultima linea");
        return steer;
    }
}

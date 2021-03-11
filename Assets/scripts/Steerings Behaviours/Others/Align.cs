using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**public class Align : SteeringBehaviour
{
   public float TimeToTarget = 1;
    private float targetRotation=0;
    private float angularAcceleration=0;
    override public Steering GetSteering(AgentNPC agent)
    {
       
        //establecer a valores nulos el steering que se debe retornar,
        Steering steer = this.gameObject.GetComponent<Steering>();
        //calculamos la distancia entre objetivo y el agente player (de un punto a otro)
        float distancia = Mathf.Sqrt(Mathf.Pow((target.transform.position.x - this.transform.position.x),2) + 
        0 +
        Mathf.Pow((target.transform.position.z - this.transform.position.z),2));

        float Rotation = target.Orientation - agent.Orientation;

        Rotation = mapToRange(Rotation);
        float RotationSize = abs(Rotation);

        if(RotationSize > agent.intRadius)
        {
            if(RotationSize > agent.extRadius){
                targetRotation = agent.maxRotation;
            } else {
                targetRotation = agent.maxRotation * RotationSize / agent.extRadius;
            }

            targetRotation *= Rotation / RotationSize;

            this.steer.angular = targetRotation - target.rotation;
            this.steer.angular /= TimeToTarget;

            angularAcceleration = abs(this.steer.angular);
            if(angularAcceleration > maxAngularAcceleration){
                this.steer.angular /= angularAcceleration;
                this.steer.angular *= maxAngularAcceleration;
            }
        }
        this.steer.linear = Vector3.zero;
        return steer;
        
    }
    
} */

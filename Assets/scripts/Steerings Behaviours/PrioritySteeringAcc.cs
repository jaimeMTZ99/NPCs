using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySteeringAcc : SteeringBehaviour
{
    public List<BlendedSteering> groups;

    public float epsilon;

    public override Steering GetSteering(AgentNPC character)
    {

        Steering steering = this.gameObject.GetComponent<Steering>();

        int i=0;
        foreach (BlendedSteering group in groups)
        {
            steering = group.GetSteering(character);
            Debug.Log(steering.linear.magnitude);
            if(steering.linear.magnitude > epsilon) 
            {
                Debug.Log(i);
                //Debug.Log(steering.linear);
                //Debug.Log(steering.angular);
                return steering;
            }
            i++;
        }

        return steering; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySteering1 : SteeringBehaviour
{
    public List<BlendedSteering> groups;

    public float epsilon;

    public override Steering GetSteering(AgentNPC character)
    {
        Steering steering = this.gameObject.GetComponent<Steering>(); 

        foreach (BlendedSteering group in groups)
        {
            steering = group.GetSteering(character);

            if(Mathf.Abs(steering.angular) > epsilon)
            {
                return steering;
            }
        }

        return steering; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySteering : SteeringBehaviour
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
            if(Mathf.Abs(steering.linear.magnitude) > epsilon) 
            {
                Debug.Log(i);
                return steering;
            }
            i++;
        
        }

        return steering; 
    }
}

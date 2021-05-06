using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySteeringAcc : SteeringBehaviour
{
    public List<BlendedSteering> groups;    //lista de comportamientos blende steering

    public float epsilon;       //umbral para el arbitro

    public override Steering GetSteering(AgentNPC character)
    {
        Steering steering = this.gameObject.GetComponent<Steering>();
        foreach (BlendedSteering group in groups)
        {
            steering = group.GetSteering(character);
            if(Mathf.Abs(steering.linear.magnitude) > epsilon) //si el linear supera el umbral, entonces lo realizamos
            {
                return steering;
            }
        }

        return steering; 
    }
}

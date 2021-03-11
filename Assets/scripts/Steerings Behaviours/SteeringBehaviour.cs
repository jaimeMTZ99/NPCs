using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{


    public Agent target;
    public int weight;

    public abstract Steering GetSteering(AgentNPC agent);
}

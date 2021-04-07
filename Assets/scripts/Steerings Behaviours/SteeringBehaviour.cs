using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{


    public Agent target;
    public float weight = 1f;

    public abstract Steering GetSteering(AgentNPC agent);
}

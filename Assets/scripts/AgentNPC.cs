﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNPC : Agent
{

    public SteeringBehaviour[] SteeringList;
    [SerializeField]
    private Steering steer;
    [SerializeField]
    public float blendWeight;

    void Awake()
    {
        SteeringList = this.gameObject.GetComponents<SteeringBehaviour>();
    }
    void LateUpdate()
    {
        foreach (SteeringBehaviour s in SteeringList)
        {
            steer = s.GetSteering(this);
        }
    }
    void Update()
    {
        applySteering(steer);

    }
    public void applySteering(Steering s)
    {
        Vector3 Acceleration = s.linear / mass;       // A = F/masa
        Rotation = s.angular;
        Position += Velocity * Time.deltaTime; // Fórmulas de Newton
        Orientation += Rotation * Time.deltaTime; //Radianes
        Velocity += Acceleration * Time.deltaTime;  // Aceleracion usando el tiempo      

        transform.rotation = new Quaternion(); //Quaternion.identity;
        transform.Rotate(Vector3.up, Orientation * Mathf.Rad2Deg);
    }
}

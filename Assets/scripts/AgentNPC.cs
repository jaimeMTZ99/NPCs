using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNPC : Agent
{
    public List<SteeringBehaviour> SteeringList;
    [SerializeField]
    private Steering steer;
    [SerializeField]
    public float blendWeight;

    void Awake()
    {
        if(this.gameObject.GetComponent<PrioritySteering>()!= null)
        {
            SteeringList.Add(this.gameObject.GetComponent<PrioritySteering>());
        }
        else if(this.gameObject.GetComponent<PrioritySteering1>() != null)
        {
            SteeringList.Add(this.gameObject.GetComponent<PrioritySteering1>());
        }
        else if (this.gameObject.GetComponent<BlendedSteering>() != null){
            SteeringList.Add(this.gameObject.GetComponent<BlendedSteering>());
        }
        else{
            foreach (SteeringBehaviour s in this.gameObject.GetComponents<SteeringBehaviour>())
            {
                SteeringList.Add(s);
            }
        }
        //SteeringList = this.gameObject.GetComponents<SteeringBehaviour>();
    }
    void FixedUpdate()
    {
        PrioritySteering ps = this.gameObject.GetComponent<PrioritySteering>();
        PrioritySteering1 ps1 = this.gameObject.GetComponent<PrioritySteering1>();
        BlendedSteering arbitro = this.gameObject.GetComponent<BlendedSteering>();

        if(ps != null){
            steer = ps.GetSteering(this);
            applySteering(steer);
        }
        else if(ps1 != null){
            steer = ps1.GetSteering(this);
            applySteering(steer);            
        } else if (arbitro == null) {
            if (SteeringList != null)
            {
                foreach (SteeringBehaviour s in SteeringList)
                {
                    steer = s.GetSteering(this);
                    applySteering(steer);
                }
            }
        }
        else {
            steer = arbitro.GetSteering(this);
            applySteering(steer);
        }
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

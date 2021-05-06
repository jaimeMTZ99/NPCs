using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNPC : Agent
{
    public List<SteeringBehaviour> SteeringList;        //lista de steerings que tiene el agente
    [SerializeField]
    private Steering steer;
    [SerializeField]
    public float blendWeight;

    void Awake()    //simplemente cogemos en funcion de si es arbitro, o solo comportamientos para tener toda la lista de steerings
    {
        if(this.gameObject.GetComponent<PrioritySteeringAcc>()!= null)
        {
            SteeringList.Add(this.gameObject.GetComponent<PrioritySteeringAcc>());
        }
        else if(this.gameObject.GetComponent<PrioritySteeringAng>() != null)
        {
            SteeringList.Add(this.gameObject.GetComponent<PrioritySteeringAng>());
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
    //activamos en funcion de si tienen arbitros o no todos los steerings behaviours
    {
        PrioritySteeringAcc ps = this.gameObject.GetComponent<PrioritySteeringAcc>();
        PrioritySteeringAng ps1 = this.gameObject.GetComponent<PrioritySteeringAng>();
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
    //funcion usada para aplicar los cambios de los steerigns a las propiedades del agente
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

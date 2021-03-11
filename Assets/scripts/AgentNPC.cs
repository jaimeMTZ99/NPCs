using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNPC : Agent
{   
    [SerializeField]
    private SteeringBehaviour[] SteeringList;
    [SerializeField]
    private Steering steer;

    [SerializeField]
    public float blendWeight;

    private Vector3 Velocity = Vector3.zero;
    private float Rotation = 0;


    void Awake(){
        SteeringList = this.gameObject.GetComponents<SteeringBehaviour>();
    }


    void LateUpdate(){
        foreach (SteeringBehaviour s in SteeringList)
        {
            steer = s.GetSteering(this);
        }
    }




    void Update(){
        applySteering(steer);

    }
    
    public void applySteering(Steering s)
    {
      
      /**
        Vector3 Acceleration = Vector3.zero;
        
        Vector3 Velocity = this.steer.linear;
        float Rotation = this.steer.angular;
        
        //Debug.Log(Rotation);

        Position += Velocity * Time.deltaTime; // Fórmulas de Newton
        Orientation += (Rotation * Mathf.Deg2Rad) * Time.deltaTime; //Radianes

        
        //Debug.Log(Orientation);
        // Pasar los valores Position y Orientation a Unity. Por ejemplo
        transform.rotation = new Quaternion(); //Quaternion.identity;
        transform.Rotate(Vector3.up, Orientation);

       **/

        Vector3 Acceleration = s.linear/mass;       // A = F/masa
        float AngularSpeed = s.angular;

        Position += Velocity * Time.deltaTime; // Fórmulas de Newton
        Orientation += (Rotation * Mathf.Deg2Rad) * Time.deltaTime; //Radianes

        if(Acceleration == Vector3.zero)
        {
            Velocity = Vector3.zero;
        }
        else
            Velocity += Acceleration* Time.deltaTime;  // Aceleracion usando el tiempo            
        
        Rotation += AngularSpeed*Time.deltaTime;
        
        //Debug.Log(Orientation);
        // Pasar los valores Position y Orientation a Unity. Por ejemplo
        transform.rotation = new Quaternion(); //Quaternion.identity;
        transform.Rotate(Vector3.up, Orientation); 


    }
}

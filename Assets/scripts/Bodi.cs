using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodi : MonoBehaviour
{

    public float speed;
    public float mass;
    public float maxSpeed;
    public float rotation;
    public float maxRotation;
    public float maxAcceleration;

    private float orientation;

    public Vector3 velocity;
    public Vector3 acceleration;

    public float Orientation
    {
        get {return orientation;}
        set {
            if (value > Mathf.PI)
            {
                orientation = Mathf.PI;
            }
            else if (value < -Mathf.PI)
            {
                orientation = -Mathf.PI;
            }
            orientation = value;
            }
    }


    public Vector3 Position
    {
        get {return transform.position;}
        set {transform.position = value;}
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float PositionToAngle(Vector3 pos)
    {
        return Mathf.Atan2(pos.x,pos.z) * Mathf.Rad2Deg;
    } 

    public Vector3 AngleToPosition(float angle)
    {
        //TODO quitar redondeo 
        return new Vector3(Mathf.Cos (angle),0,Mathf.Sin (angle));
    } 

    public Vector3 directionToTarget(Vector3 pos){
        Vector3 vecDir;
        vecDir = pos-this.transform.position;
        return vecDir;
    }
    public float Heading(Vector3 pos){

        return PositionToAngle(directionToTarget(pos));
    }

    public float nuevaOrientacion(float orientacion, Vector3 velocidad){
        if(velocidad != Vector3.zero)
            return Mathf.Atan2(-velocidad.x,velocidad.z);
        
        return orientacion;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Bodi
{
    public float intRadius;
    public float extRadius;
    public float intAngle;
    public float extAngle;

    public bool gizmosIntRadius;
    public bool gizmosExtRadius;
    public bool gizmosIntAngle;
    public bool gizmosExtAngle;
    


    // Update is called once per frame
    void Update()
    {
        if (intRadius > extRadius){
            intRadius = extRadius;
        } 
        if (intRadius <0){
            intRadius=0;
        }
        if (extRadius <0){
            extRadius=0;
        }

        if(intAngle > extAngle)
        {
            intAngle = extAngle;
        }

        if (intAngle <0){
            intAngle=0;
        }
        if (extAngle <0){
            extAngle=0;
        }

    }


    void OnDrawGizmosSelected()
    {
        if(gizmosIntRadius){
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, intRadius);
        }

        if(gizmosExtRadius){
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, extRadius);
        }

        if(gizmosIntAngle){
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, new Vector3(Mathf.Sin((intAngle/2)*Mathf.Deg2Rad),transform.position.y,Mathf.Cos((intAngle/2)*Mathf.Deg2Rad)));

            Gizmos.DrawLine(transform.position, new Vector3(-(Mathf.Sin((intAngle/2)*Mathf.Deg2Rad)),transform.position.y,Mathf.Cos((intAngle/2)*Mathf.Deg2Rad)));
        }

        if(gizmosExtAngle){
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, new Vector3(Mathf.Sin((extAngle/2)*Mathf.Deg2Rad),transform.position.y,Mathf.Cos((extAngle/2)*Mathf.Deg2Rad)));

            Gizmos.DrawLine(transform.position, new Vector3(-(Mathf.Sin((extAngle/2)*Mathf.Deg2Rad)),transform.position.y,Mathf.Cos((extAngle/2)*Mathf.Deg2Rad)));
        }

    }


}

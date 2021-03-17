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
            Quaternion intRot1 = Quaternion.Euler(0, intAngle*Mathf.Rad2Deg, 0);
            Quaternion intRot2 = Quaternion.Euler(0, -intAngle*Mathf.Rad2Deg, 0);
            Gizmos.color = Color.blue;
            var position = transform.position;
            var forward = transform.forward;
            Gizmos.DrawLine(position, position + intRot1 * forward);
            Gizmos.DrawLine(position, position + intRot2 * forward);
        }

        if(gizmosExtAngle){
            Quaternion extRot1 = Quaternion.Euler(0, extAngle*Mathf.Rad2Deg, 0);
            Quaternion extRot2 = Quaternion.Euler(0, -extAngle*Mathf.Rad2Deg, 0);
            Gizmos.color = Color.red;
            var position = transform.position;
            var forward = transform.forward;
            Gizmos.DrawLine(position, position + extRot1 * forward);
            Gizmos.DrawLine(position, position + extRot2 * forward);
        }

    }


}

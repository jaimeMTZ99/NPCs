using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowingPredict : SeekAcceleration
{
    public Path path;
    public int targetParam;
    public int currentPos;
    public Vector3 futurePos;
    public int currentParam;
    public  Agent aux;

    public float predictTime = 0.1f;
    public GameObject goPathFoll;
    void Start(){
        goPathFoll = new GameObject("PathFollowing");
        Agent invisible = goPathFoll.AddComponent<Agent>() as Agent;
        invisible.intRadius = path.Radio;
        invisible.extRadius = path.Radio + 0.5f;
        target = invisible;
        currentPos = 0;
    }
    

    public override Steering GetSteering(AgentNPC agent){

        futurePos = agent.transform.position+agent.Velocity*predictTime;
        //Actual posición en el camino
        currentParam = path.GetParam(futurePos, currentPos);
        //Actualizamos la posición actual
        currentPos = currentParam;
        //Calculamos la posición del target en el camino.
        targetParam = currentParam + 1;
        //Calculamos la posición del keypoint target.
        base.target.transform.position = path.GetPosition(targetParam);
        return base.GetSteering(agent);
    }

}

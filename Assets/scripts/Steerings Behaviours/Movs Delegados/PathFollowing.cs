using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : SeekAcceleration
{

    [SerializeField]
    public Path path;

    public int targetParam;

    [SerializeField]
    public int currentPos;

    [SerializeField]
    public int currentParam;

    [SerializeField]
    public  Agent aux;

    public GameObject goPathFoll;
    void Start(){
        Debug.Log("start");
        goPathFoll = new GameObject("PathFollowing");
        Agent invisible = goPathFoll.AddComponent<Agent>() as Agent;
        invisible.intRadius = path.Radio;
        invisible.extRadius = path.Radio + 0.5f;
        target = invisible;
        currentPos = 0;
    }
    

    public override Steering GetSteering(AgentNPC agent){
        //Actual posición en el camino
        currentParam = path.GetParam(agent.transform.position, currentPos);
        //Actualizamos la posición actual
        currentPos = currentParam;
        //Calculamos la posición del target en el camino.
        targetParam = currentParam + 1;
        //Calculamos la posición del keypoint target.
        base.target.transform.position = path.GetPosition(targetParam);
        return base.GetSteering(agent);
    }

}

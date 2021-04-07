using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : SeekAcceleration
{

    [SerializeField]
    private Path path;

    [SerializeField]
    private int pathOffset;

    private int targetParam;

    [SerializeField]
    private int currentPos;

    [SerializeField]
    private int currentParam;

    [SerializeField]
    private Agent aux;

    private GameObject goPathFoll;
    void Start(){
        goPathFoll = new GameObject("PathFollowing");
        Agent invisible = goPathFoll.AddComponent<Agent>() as Agent;
        invisible.intRadius = path.Radio;
        invisible.extRadius = path.Radio + 0.5f;
        target = invisible;
        /*
        aux = target;
        this.target = invisible;
        target.intRadius = aux.intRadius;
        target.extRadius = aux.extRadius;
        */
        currentPos = 0;
    }
    

    public override Steering GetSteering(AgentNPC agent){
        //Actual posición en el camino
        currentParam = path.GetParam(agent.transform.position, currentPos);
        //Actualizamos la posición actual
        currentPos = currentParam;
        //Calculamos la posición del target en el camino.
        targetParam = currentParam + pathOffset;
        //Calculamos la posición del keypoint target.
        base.target.transform.position = path.GetPosition(targetParam);
        return base.GetSteering(agent);
    }

}

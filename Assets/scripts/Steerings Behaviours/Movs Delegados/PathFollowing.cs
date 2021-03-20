using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : SeekAcceleration
{

    public Path path;

    [SerializeField]
    private float pathOffset;
    public float targetParam;
    protected float currentPos = 0;
    protected float currentParam;


    public override Steering GetSteering(AgentNPC agent){
    
        currentParam = path.GetParam(agent.transform.position, currentPos);
        targetParam = currentParam + pathOffset;
        target.transform.position = path.GetPosition(targetParam);
        return base.GetSteering(agent);
    }

}

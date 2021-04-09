using UnityEngine;

public class LookWhereYouGoing : Align {
    
    [Header("LookWhereYouGoing")]
    // Alternative, used for the strategy scene
    [SerializeField]
    private GameObject goLook;
    void Start(){
         goLook = new GameObject("LookWhereYouGoing");
         target = goLook.AddComponent<Agent>() as Agent;
    }
    
    public override Steering GetSteering(AgentNPC agent) {
        if (agent.Velocity.magnitude == 0){
            target.orientation = agent.orientation;
        }
        target.orientation = Mathf.Atan2(-agent.velocity.x, agent.velocity.z);

        return base.GetSteering(agent);
    }
}

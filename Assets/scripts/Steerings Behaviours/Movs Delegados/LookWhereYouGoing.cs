using UnityEngine;

public class LookWhereYouGoing : Align {
    
    [Header("LookWhereYouGoing")]
    // Alternative, used for the strategy scene
    [SerializeField]
    private AgentNPC externalTarget;
    
    public override Steering GetSteering(AgentNPC agent) {
        if (externalTarget != null) {
            if (externalTarget.Velocity.magnitude < 0.05) {
                // Return "none"
                targetRotation = agent.Orientation;
            } else {
                targetRotation = Mathf.Atan2(externalTarget.Velocity.x, externalTarget.Velocity.z);
            }
            return base.GetSteering(agent);
        }

        if (agent.Velocity.magnitude < 0.05) {
            // Return "none"
            targetRotation = agent.Orientation;
        } else {
            targetRotation = Mathf.Atan2(agent.Velocity.x, agent.Velocity.z);
        }
        return base.GetSteering(agent);
    }
}

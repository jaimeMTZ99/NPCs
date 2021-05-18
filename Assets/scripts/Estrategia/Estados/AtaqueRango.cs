using UnityEngine;

public class AtaqueRango : Estado  {

    private float time;
    private bool pointless;
    public override void EntrarEstado(NPC npc) {
        time = -1f;
        pointless = false;
        npc.GetComponent<Path>().ClearPath();
    }

    public override void SalirEstado(NPC npc) {
    }

    public override void Accion(NPC npc) {

        
        Face f = npc.GetComponent<Face>();
        if (f == null){
            npc.gameObject.AddComponent<Face>();
            npc.gameObject.GetComponent<Face>().target = npcObjetivo.gameObject.GetComponent<AgentNPC>();
        }
        else{
            f.target = npcObjetivo.gameObject.GetComponent<AgentNPC>();
        }
        // To make a ranged attack on an enemy, I need to have a straight shooting line and the target must be within my range
        // Also, if I am low health and I am not in total war, I should not commit to shooting
        if (!UnitsManager.DirectLine(npc, npcObjetivo)
            || Vector3.Distance(npc.agentNPC.Position, npcObjetivo.agentNPC.Position) > npc.rangedRange
            || (!npc.gameManager.totalWarMode && npc.health <= npc.menosVida))
            pointless = true;
        if (!pointless) {
            // Start winding up my attack
            if (time == -1) {
                time = Time.time;
            }
            // Wait patiently
            if (Time.time - time >= npc.rangedAttackSpeed) {
                CombatManager.AtaqueRango(npc, npcObjetivo);
                time = -1;
            }
        }
    }

    public override void Ejecutar(NPC npc) {
        Accion(npc);
        ComprobarEstado(npc);
    }

    public override void ComprobarEstado(NPC npc) {
        Face f = npc.GetComponent<Face>();
        f.target = null;
        if (ComprobarMuerto(npc)){
            f.target = null;
            f.aux = null;
            return;
        }
        if (!pointless && (ComprobarAtaqueRangoMedico(npc) || ComprobarAtaqueRangoMelee(npc))){
            return;
        f.target = null;
        f.aux = null;
        }
        npc.CambiarEstado(npc.estadoAsignado);
    }
}
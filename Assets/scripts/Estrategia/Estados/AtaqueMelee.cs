using UnityEngine;

public class AtaqueMelee : Estado  {

    private float time;
    private bool iTried;
    private bool pointless;
    public override void EntrarEstado(NPC npc) {
        move = false;
        iTried = false;
        pointless = false;
        time = -0.5f;
    }

    public override void SalirEstado(NPC npc) {
        npc.GetComponent<Path>().ClearPath();
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
        // To melee attack an enemy, he must be within my range
        float distance = Vector3.Distance(npc.agentNPC.Position, npcObjetivo.agentNPC.Position);
        Path camino = npc.GetComponent<Path>();
        int posicionActualCamino = npc.GetComponent<PathFollowing>().currentPos;
        bool isFinalCamino = camino.EndOfThePath(posicionActualCamino);
        if (distance <= npc.rangoMelee) {

            // He is within my range, stop moving
            if (move) {
                move = false;
                npc.GetComponent<Path>().ClearPath();
            }
            // I can start winding up my attack
            if (time == -0.5f) {
                if (!npc.gameManager.totalWarMode && npc.health <= npc.menosVida) {
                    // But I am on low HP and I am not in total war, so I should leave
                    pointless = true;
                    return;
                }
                time = Time.time;
            }
            
            // Wait patiently
            if (Time.time - time >= npc.meleeAttackSpeed) {
                CombatManager.AtaqueMelee(npc, npcObjetivo);
                time = -0.5f;
                iTried = false;
            }
        } else {
            // He is not within my range
            if (!npc.gameManager.totalWarMode && npc.health <= npc.menosVida) {
                // I happen to be low health and I am not in total war, so it's not worth commiting
                pointless = true;
                return;
            }
            
            if (!move && !iTried && time == -1) {
                // I'm not moving and I haven't tried chasing him
                npc.pf.EncontrarCaminoJuego(npc.nodoActual.Posicion, npcObjetivo.nodoActual.Posicion);
                move = true;
                iTried = true;
            }
            else if (isFinalCamino && iTried) {
                pointless = true;
            }
        }
    }

    public override void Ejecutar(NPC npc) {
        Accion(npc);
        ComprobarEstado(npc);
    }

    public override void ComprobarEstado(NPC npc) {
        Face f = npc.GetComponent<Face>();  
        // If the unit is dead, change to that state
        if (ComprobarMuerto(npc)){
            return;
            f.target = null;
            f.aux = null;
            }
        if (!pointless && ComprobarAtaqueRangoMelee(npc)){
            return;
            f.target = null;
            f.aux = null;
            }
        npc.CambiarEstado(npc.estadoAsignado);
    }


}
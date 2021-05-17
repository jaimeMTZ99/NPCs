using UnityEngine;

public class Curar : Estado {
    private bool healed;
    private bool pointless;
    private float timer;
    private float healingRate = 1;

    public override void EntrarEstado(NPC npc) {
        npc.GetComponent<Path>().ClearPath();
        timer = -1;
    }

    public override void SalirEstado(NPC npc) {
        healed = false;
        pointless = false;
    }

    public override void Accion(NPC npc) {
        // If the NPC is at base, start healing until max hp
        if (npc.gameManager.InCuracion(npc)) {
            if (timer == -1)
                timer = Time.time;

            if (Time.time - timer >= 1) {
                timer = -1;
                if (npc.health < npc.maxVida)
                    npc.health += 10;
                else {
                    healed = true;
                }
            }

        } else {
            // Otherwise, get healed until it is acceptable by the medic
            if (npc.health <= npc.healthy) {
                // If the medic has died, abort
                NPC closestMedic = UnitsManager.MedicoCerca(npc);
                if (closestMedic == null || Vector3.Distance(npc.agentNPC.Position, closestMedic.agentNPC.Position) > closestMedic.rangedRange) {
                    pointless = true;
                }
            } else
                healed = true;
        }
    }

    public override void Ejecutar(NPC npc) {
        Accion(npc);
        ComprobarEstado(npc);
    }

    public override void ComprobarEstado(NPC npc) {
        if (ComprobarMuerto(npc))
            return;
        
        // Medic is dead, run to base
        if (pointless)
            npc.CambiarEstado(npc.estadoAsignado);
        
        if (healed) {
            npc.CambiarEstado(npc.estadoAsignado);
        }

    }

}
using UnityEngine;

public class Patrullar : Estado  {

    private bool patrolling;
    private float minDistance = 10f;
    public override void EntrarEstado(NPC npc) {
        move = false;
        patrolling = false;
        npc.GetComponent<PathFollowing>().patrol = true;
    }

    public override void SalirEstado(NPC npc) {
        npc.GetComponent<PathFollowing>().patrol = false;
        npc.GetComponent<Path>().ClearPath();
    }

    public override void Accion(NPC npc) {
        float distanceToBeginning = Vector3.Distance(npc.agentNPC.Position, npc.puntoPatrullaInicial.position);
        float distanceToEnd = Vector3.Distance(npc.agentNPC.Position, npc.puntoPatrullaFin.position);
        if (!move) {
            if (distanceToBeginning < distanceToEnd)
                npc.pf.EncontrarCaminoJuego(npc.nodoActual.Posicion, npc.puntoPatrullaInicial.position);
            else 
                npc.pf.EncontrarCaminoJuego(npc.nodoActual.Posicion, npc.puntoPatrullaFin.position);
            move = true;
        }
        else if (distanceToBeginning <= minDistance || distanceToEnd <= minDistance) {
            if (!patrolling) {
                npc.GetComponent<PathFollowing>().patrol = true;
                if (distanceToBeginning <= minDistance)
                    npc.pf.EncontrarCaminoJuego(npc.puntoPatrullaInicial.position, npc.puntoPatrullaFin.position);
                else if (distanceToEnd <= minDistance)
                    npc.pf.EncontrarCaminoJuego(npc.puntoPatrullaFin.position, npc.puntoPatrullaInicial.position);
                patrolling = true;
            }
        }
    }

    public override void Ejecutar(NPC npc) {
        Accion(npc);
        ComprobarEstado(npc);
    }

    public override void ComprobarEstado(NPC npc) {

        GameManager gameManager = npc.gameManager;

        // If the unit is dead, change to that state
        if (ComprobarMuerto(npc))
            return;
        // If there are too many enemies, flee
        if (ComprobarEscapar(npc))
            return;
        // Otherwise, if I am a medic, check if there are nearby allies to "shoot" (heal) and if so, shoot them
        if (ComprobarAtaqueRangoMedico(npc))
            return;
        // Otherwise, check first if I have to defend my capture point
        if (ComprobarDefensa(gameManager, npc))
            return;
        // Otherwise, check if I can capture the enemy point
        if (ComprobarCaptura(gameManager, npc))
            return;
        // Otherwise, check if I can attack any enemies
        if (ComprobarAtaqueRangoMelee(npc))
            return;
    }
}
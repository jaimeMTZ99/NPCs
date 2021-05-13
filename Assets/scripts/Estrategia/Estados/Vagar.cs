using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Vagar : Estado {

    public override void EntrarEstado(NPC npc) {
        move = false;
    }

    public override void SalirEstado(NPC npc) {
        npc.GetComponent<Path>().ClearPath();
    }

    public override void Accion(NPC npc) {

        GameManager gameManager = npc.gameManager;
        if (!move) {
            if (npc.team == NPC.Equipo.Spain)
                npc.pf.EncontrarCaminoJuego(npc.nodoActual.Posicion, gameManager.waypointManager.GetNodoAleatorio(gameManager.waypointManager.vagarWaypointSPA).Posicion);
            else
                npc.pf.EncontrarCaminoJuego(npc.nodoActual.Posicion, gameManager.waypointManager.GetNodoAleatorio(gameManager.waypointManager.vagarWaypointFRA).Posicion);
            move = true;
        }
        Path pathNPC = npc.agentNPC.GetComponent<Path>();
        if (npc.GetComponent<PathFollowing>().EndOfThePath() || Vector3.Distance(npc.agentNPC.Position,pathNPC.nodos[pathNPC.nodos.Count-1].gameObject.transform.position) < 4){
            Debug.Log("Terminando vagar");
            move = false;
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
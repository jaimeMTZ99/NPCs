using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AsignarEstado : Estado  {

    public override void EntrarEstado(NPC npc) {
        
    }

    public override void SalirEstado(NPC npc) {
       
    }

    public override void Accion(NPC npc) {
    }

    public override void Ejecutar(NPC npc) {
        Accion(npc);
        ComprobarEstado(npc);
    }

    public override void ComprobarEstado(NPC npc) {

        GameManager gameManager = npc.gameManager;
        // If the unit is dead, change to that state
        if (ComprobarMuerto(npc))
        {
            Debug.Log("Muerto " + npc.name);
            return;
        }
        // If there are too many enemies, flee
        if (ComprobarEscapar(npc))
        {
            Debug.Log("Escapar " + npc.name);
            return;
        }
        // Otherwise, if I am a medic, check if there are nearby allies to "shoot" (heal) and if so, shoot them
        if (ComprobarAtaqueRangoMedico(npc))
        {
            Debug.Log("ranged " + npc.name);
            return;
        }
        // Otherwise, check first if I have to defend my capture point
        if (ComprobarDefensa(gameManager, npc))
        {
            Debug.Log("Defendiendo " + npc.name);
            return;
        }
        // Otherwise, check if I can capture the enemy point
        if (ComprobarCaptura(gameManager, npc))
        {
            Debug.Log("capturando " + npc.name);
            return;
        }
        // Otherwise, check if I can attack any enemies
        if (ComprobarAtaqueRangoMelee(npc)){
        {
            Debug.Log("Pegando " + npc.name);
            return;
        }
        }
        // If I cannot do any of these tasks and I have to patrol, then patrol
        if (npc.patrol)
        {
            Debug.Log("Patrullando " + npc.name);
            npc.CambiarEstado(npc.estadoPatrullar);
        }

        else {
            // Otherwise, roam around our "country"
            Debug.Log("roaming " + npc.name);
            npc.CambiarEstado(npc.estadoVagar);
        }
    }

}
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
        // Otherwise, check if I have to reload
        if (ComprobarRecarga(npc))
            return;
        
        //TODO ¿NECESARIO?
        // If I cannot do any of these tasks and I have to patrol, then patrol
       /* if (npc.patrol)
            npc.CambiarEstado(npc.PatrolState);
        else {
            // Otherwise, roam around our "country"
            npc.CambiarEstado(npc.RoamState);
        }*/
    }

}
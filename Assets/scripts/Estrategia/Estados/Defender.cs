public class Defender : Estado  {

    public override void EntrarEstado(NPC npc) {
        move = false;
    }

    public override void SalirEstado(NPC npc) {
        npc.GetComponent<Path>().ClearPath();
    }

    public override void Accion(NPC npc) {

        if (!move) {
            // Go to our capture point
            npc.pf.EncontrarCaminoJuego(npc.nodoActual.Posicion, npc.gameManager.waypointManager.GetNodoAleatorio(npc.gameManager.waypointManager.GetEquipo(npc)).Posicion);
            move = true;
        }

    }

    public override void Ejecutar(NPC npc) {
        Accion(npc);
        ComprobarEstado(npc);
    }

    public override void ComprobarEstado(NPC npc) {
        GameManager gameManager = npc.gameManager;

        if (ComprobarMuerto(npc))
            return;
        if (ComprobarAtaqueRangoMedico(npc))
            return;
        if (ComprobarDefensa(gameManager, npc))
            return;
            
       npc.CambiarEstado(npc.estadoAsignado);
    }

}
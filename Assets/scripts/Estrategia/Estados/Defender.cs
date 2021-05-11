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
            //npc.pf.FindPathToPosition(npc.CurrentTile.worldPosition, 
           // npc.gameManager.waypointManager.GetRandomTile(npc.gameManager.waypointManager.GetAlliedCheckpoint(npc)).worldPosition);
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
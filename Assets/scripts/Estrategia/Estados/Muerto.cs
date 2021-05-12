using UnityEngine;

public class Muerto : Estado  {

    private float deadTime = 10f;
    private float time;

    public override void EntrarEstado(NPC npc) {
        //npc.SimplePropagator.Value = 0;
        npc.GetComponent<Path>().ClearPath();
        move = false;
        time = Time.time;
    }

    public override void SalirEstado(NPC npc) {
        if (npc.team == NPC.Equipo.Spain){} 
           // npc.SimplePropagator.Value = 1;
        else{}
           // npc.SimplePropagator.Value = -1;
    }

    public override void Accion(NPC npc) {
        // When it is finally time to come back from the dead, respawn at base
        if (Time.time - time >= deadTime) {
            npc.agentNPC.Position = npc.gameManager.waypointManager.GetNodoAleatorio(npc.gameManager.waypointManager.GetBase(npc)).Posicion;
            npc.health = npc.maxVida;
            npc.municionActual = npc.maxMunicion;
        }
    }

    public override void Ejecutar(NPC npc) {
        Accion(npc);
        ComprobarEstado(npc);
    }

    public override void ComprobarEstado(NPC npc) {
        // I have returned from the dead, get back to business
        if (npc.health > 0)
            npc.CambiarEstado(npc.estadoAsignado);
    }
}

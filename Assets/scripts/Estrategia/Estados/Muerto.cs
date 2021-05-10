using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerto : Estado
{
    public float timepoMuerto = 8;
    private float timeRes;

    public override void EntrarEstado(NPC npc) {
        //npc.SimplePropagator.Value = 0;
        //npc.Pathfinding.ClearPath();
        move = false;
        timeRes = Time.time;
    }

    public override void SalirEstado(NPC npc) {

        //volver a establecer mapa influencias

        /**if (npc.Team == NPC.UnitTeam.Red) 
            npc.SimplePropagator.Value = 1;
        else
            npc.SimplePropagator.Value = -1;**/
    }

    public override void Accion(NPC npc) {
        // respawnear en la base cunado el tiempo se agote
        if (Time.time - timeRes > timepoMuerto) {
            //npc.AgentNPC.Position = npc.GameManager.WaypointManager.GetRandomTile(npc.GameManager.WaypointManager.GetAlliedBase(npc)).worldPosition;
            //npc.Health = npc.MaxHealth;
        }
        //if (npc.Health > 0){

        //}
            //npc.ChangeState(npc.IdleState);
    }

}

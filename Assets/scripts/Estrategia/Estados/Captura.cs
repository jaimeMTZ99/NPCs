using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captura : Estado
{

    public override void EntrarEstado(NPC npc) {
        move = false;
    }

    public override void SalirEstado(NPC npc) {
        //npc.Pathfinding.ClearPath();
    }

    public override void Accion(NPC npc) {
/*
        GameManager gameManager = npc.GameManager;
        
        // If the unit is already at the enemy capture point, stop moving
        if (gameManager.NPCInWaypoint(npc, gameManager.WaypointManager.GetEnemyCheckpoint(npc))) {
            if (move) {
                move = false;
                //npc.Pathfinding.ClearPath();
            }

            // If there are no enemies defending their capture point, increment the capture bar
           // if (!gameManager.EnemiesDefending(npc))
            //    gameManager.WaypointManager.Capturing(npc);
        } 
        // Otherwise, start moving towards the enemy capture point
        else if (!move) {
            // The target position is one of the random available positions within the enemy capture point
            npc.Pathfinding.FindPathToPosition(npc.CurrentTile.worldPosition, gameManager.WaypointManager.GetRandomTile(gameManager.WaypointManager.GetEnemyCheckpoint(npc)).worldPosition);
            move = true;
        }

        // If the unit is dead, change to that state
        if (isDead(npc))
            return;
        //if (CheckMedicRangedAttack(npc))
            //return;
        npc.ChangeState(npc.IdleState);
**/
    }
/*
    protected bool isDead(NPC npc) {
        if (npc.Health == 0) {
            npc.ChangeState(npc.DeadState);
            return true;
        }
        return false;
    } **/

}

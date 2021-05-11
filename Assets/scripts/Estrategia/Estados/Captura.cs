using UnityEngine;

public class Captura : Estado {

    private bool pointless;
    public override void EntrarEstado(NPC npc) {
        move = false;
        pointless = false;
    }

    public override void SalirEstado(NPC npc) {
        npc.GetComponent<Path>().ClearPath();
    }

    public override void Accion(NPC npc) {

        GameManager gameManager = npc.gameManager;
        
        // If the unit is already at the enemy capture point, stop moving
        if (gameManager.NPCInWaypoint(npc, gameManager.waypointManager.GetEnemyCheckpoint(npc))) {
            if (move) {
                move = false;
                npc.GetComponent<Path>().ClearPath();
            }

            // If there are no enemies defending their capture point, increment the capture bar
            if (!gameManager.EnemigosDefendiendo(npc))
                //gameManager.waypointManager.Capturing(npc);
        } 
        // Otherwise, start moving towards the enemy capture point
        else if (!move) {
            // The target position is one of the random available positions within the enemy capture point
            
            //npc.Pathfinding.FindPathToPosition(npc.CurrentTile.worldPosition, gameManager.WaypointManager.GetRandomTile(gameManager.WaypointManager.GetEnemyCheckpoint(npc)).worldPosition);
            move = true;
        } else {
            // If I am on my way to the enemy capture point but it happens that there are enemies attacking our point
            if (gameManager.EnemigosCheckpoint(npc) > 0) {
                var alliedCapturePoint = gameManager.waypointManager.GetAlliedCheckpoint(npc).Position;
                var enemyCapturePoint = gameManager.waypointManager.GetEnemyCheckpoint(npc).Position;
                var currentPosition = npc.nodoActual.Posicion;
                var distanceToEnemyCapturePoint = Vector3.Distance(alliedCapturePoint, currentPosition);
                var distanceToAlliedCapturePoint = Vector3.Distance(enemyCapturePoint, currentPosition);
                if (distanceToAlliedCapturePoint <= distanceToEnemyCapturePoint) {
                    // If I am closer to our capture point, go defend
                    // Otherwise, commit to capture
                    pointless = true;
                }

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
        if (ComprobarAtaqueRangoMedico(npc))
            return;
        // Otherwise, check if I can continue capturing
        if (!pointless && ComprobarCaptura(gameManager, npc))
            return;
        npc.CambiarEstado(npc.estadoAsignado);
    }

}

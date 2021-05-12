using UnityEngine;

public class Escapar : Estado {

    private bool goHeal;
    private bool pointless;
    private bool lowHealth;
    private NPC closestMedic;
    private Nodo alliedBase;
    private Nodo startingAllyNodo;
    private Nodo startingMedicNodo;
    private float distanceToMedic;
    private float distanceToAlly;
    private float distanceToBase;
    
    public override void EntrarEstado(NPC npc) {
        lowHealth = npc.health <= npc.menosVida;
        NPC closestAlly = null;
        if (lowHealth) {
            closestMedic = UnitsManager.ClosestMedic(npc);
        }
        else {
            closestAlly = UnitsManager.ClosestAlly(npc);
        }
        alliedBase = npc.gameManager.waypointManager.GetNodoAleatorio(npc.gameManager.waypointManager.GetBase(npc));
    
        distanceToBase = Vector3.Distance(npc.nodoActual.Posicion, alliedBase.Posicion);
        if (closestMedic) {
            distanceToMedic = Vector3.Distance(closestMedic.agentNPC.Position, npc.agentNPC.Position);
            startingMedicNodo = closestMedic.nodoActual;
        }
        else
            distanceToMedic = float.MaxValue;

        if (closestAlly != null) {
            startingAllyNodo = closestAlly.nodoActual;
            distanceToAlly = Vector3.Distance(npc.nodoActual.Posicion, startingAllyNodo.Posicion);
        }
        else
            distanceToAlly = float.MaxValue;
        move = false;
        goHeal = false;
        pointless = false;
    }

    public override void SalirEstado(NPC npc) {
        npc.GetComponent<Path>().ClearPath();
    }

    public override void Accion(NPC npc) {
        // There are two reasons to escape: too many enemies or low health
        if (lowHealth) {
            // Low health, decide whether to go to base or to a medic
            // There are two possible routes to escape: my base, the closest ally or a medic
            if (!move) {
                move = true;
                if (distanceToMedic < distanceToBase && npc.tipo != NPC.TipoUnidad.Medic) {
                    // If I am closer to the medic, go to the medic
                    //npc.Pathfinding.FindPathToPosition(npc.CurrentTile.worldPosition, _closestMedic.CurrentTile.worldPosition);
                    return;
                }
                // Otherwise, go to base
                //npc.Pathfinding.FindPathToPosition(npc.CurrentTile.worldPosition, _alliedBase.worldPosition);
            } else {
                if (distanceToMedic < distanceToBase && npc.tipo != NPC.TipoUnidad.Medic) {
                    // I was headed towards my medic
                    if (npc.nodoActual == startingMedicNodo) {
                        // I have reached where my medic is supposed to be
                        NPC currentClosestMedic = UnitsManager.ClosestMedic(npc);
                        if (currentClosestMedic == null ||
                         Vector3.Distance(npc.agentNPC.Position, currentClosestMedic.agentNPC.Position) > currentClosestMedic.rangedRange || 
                         currentClosestMedic.municionActual == 0) {
                            // My medic isn't there or he has no ammo, then think again
                            pointless = true;
                        }
                        else {
                            // Let yourself get healed
                            goHeal = true;
                        }
                    }
                }
                else {
                    // I was headed towards base
                    if (npc.nodoActual == alliedBase) {
                        // I have reached my position
                        goHeal = true;
                    }
                }
            }
        }
        else {
            // Too many enemies
            // There are two possible routes to escape: my base or the closest ally
            if (!move) {
                move = true;
                if (distanceToAlly < distanceToBase) {
                    // If I am closer to the last known position of the ally, go there
                    //npc.Pathfinding.FindPathToPosition(npc.CurrentTile.worldPosition, _startingAllyTile.worldPosition);
                    return;
                }

                // Otherwise, go to base
                //npc.Pathfinding.FindPathToPosition(npc.CurrentTile.worldPosition, _alliedBase.worldPosition);
            } else {
                if (distanceToAlly < distanceToBase) {
                    // I was headed to an ally
                    if (npc.nodoActual == startingAllyNodo) {
                        // I have reached my destination
                        if (UnitsManager.EnemiesNearby(npc) > 0) {
                            // There are enemies at the position of my ally, escape to base 
                            startingAllyNodo = alliedBase;
                            npc.GetComponent<Path>().ClearPath();
                           // npc.Pathfinding.FindPathToPosition(npc.CurrentTile.worldPosition, _alliedBase.worldPosition);
                        }
                        else {
                            if (startingAllyNodo == alliedBase) {
                                // The first attempt to escape failed, we are now at base, so might as well heal
                                goHeal = true;
                            }
                            else {
                                pointless = true;
                            }
                        }
                    }
                }
                else {
                    // I was headed to base
                    if (npc.nodoActual == alliedBase) {
                        // I have reached my destination, might as well heal
                        goHeal = true;
                    }
                }
            } 
        }
    }

    public override void Ejecutar(NPC npc) {
        Accion(npc);
        ComprobarEstado(npc);
    }

    public override void ComprobarEstado(NPC npc) {
        if (ComprobarMuerto(npc))
            return;
        
        if (goHeal)
            npc.CambiarEstado(npc.estadoCuracion);
        
        if (pointless)
            npc.CambiarEstado(npc.estadoAsignado);
        
    }

}
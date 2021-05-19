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
        alliedBase = npc.gameManager.waypointManager.GetNodoAleatorio(npc.gameManager.waypointManager.GetCuracion(npc));
        distanceToBase = Vector3.Distance(npc.nodoActual.Posicion, alliedBase.Posicion);
        move = false;
        goHeal = false;
        pointless = false;
    }

    public override void SalirEstado(NPC npc) {
        npc.GetComponent<Path>().ClearPath();
    }

    public override void Accion(NPC npc) {
        if (lowHealth) {
            if (!move) {
                move = true;
                npc.pf.EncontrarCaminoJuego(npc.nodoActual.Posicion, alliedBase.Posicion);
            } else {
                if (npc.health <= npc.maxVida){
                    goHeal = true;
                }
                else {
                    pointless = true;
                }
            }
        }
        else {
            if (!move) {
                move = true;
                npc.pf.EncontrarCaminoJuego(npc.nodoActual.Posicion, alliedBase.Posicion);
            } else{
                if (npc.health <= npc.maxVida/2 ){
                    goHeal = true;
                }
                else
                    pointless = true;
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
        if (goHeal){
            npc.CambiarEstado(npc.estadoCuracion);
        }
        if (pointless) {
            npc.CambiarEstado(npc.estadoAsignado);
        }
        
        
    }

}
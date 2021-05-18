using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Estado 
{

    public NPC npcObjetivo;         //el objetivo cuando realice algun ataque
    public bool move = false;          //para indicar si se esta moviendo 

    public Estado(){
        npcObjetivo = null;
        move = false;
    }
    public abstract void Accion(NPC n);
    public abstract void EntrarEstado(NPC n);
    public abstract void SalirEstado(NPC n);
    public abstract void Ejecutar(NPC n);
    public abstract void ComprobarEstado(NPC n);

    public void SetObjective(NPC newObjective) {
        npcObjetivo = newObjective;
    }
    
    protected bool ComprobarMuerto(NPC npc) {
        // Self-explanatory
        if(npc.user)
            return false;
        if (npc.health <= 0) {
            npc.CambiarEstado(npc.estadoMuerto);
            return true;
        }
        return false;
    }

    protected bool ComprobarDefensa(GameManager gameManager, NPC npc) {
        if(npc.user)
            return false;
        if (gameManager.EnemigosCheckpoint(npc) > 0 && 
        (npc.health > npc.menosVida || gameManager.totalWarMode) 
        && !gameManager.NPCInWaypoint(npc, gameManager.waypointManager.GetEquipo(npc))) {
            // If there are enemies in our capture point and I have enough health or it's total war, go defend
            npc.CambiarEstado(npc.estadoDefensa);
            return true;
        }
        return false;
    }

    protected bool ComprobarCaptura(GameManager gameManager, NPC npc) {
        if(npc.user)
            return false;
        if ((!npc.patrol || npc.patrol && gameManager.totalWarMode) && 
        UnitsManager.EnemigosCerca(npc) == 0 && npc.health > npc.menosVida) {
            // If I'm not supposed to patrol or it's total war mode and there are no nearby enemies and I have enough health
            if (gameManager.AliadosCapturando(npc) >= npc.minAliadosCaptura) {
                // If there are enough allies capturing, go capture too
                npc.CambiarEstado(npc.estadoCaptura);
                return true;
            }
        }
        return false;
    }

    protected bool ComprobarEscapar(NPC npc) {
        if(npc.user)
            return false;
        // There is no running away in total war mode
        if (npc.gameManager.totalWarMode)
            return false;
        
        if (npc.health <= npc.menosVida || UnitsManager.EnemigosCerca(npc) > npc.numEnemigosEscape) {
            Debug.Log("Entrando en estado escapar");
            // If I have low health or there are too many enemies, flee
            npc.CambiarEstado(npc.estadoEscapar);
            return true;
        }
        return false;
    }

    protected bool ComprobarAtaqueRangoMedico(NPC npc) {
        if(npc.user)
            return false;
        if (npc.tipo == NPC.TipoUnidad.Medic) {
            // If I am medic
            NPC allyChoosen = UnitsManager.ElegirAliado(npc);
            if (allyChoosen != null) {
                if (npc.name == "MedicoFra"){
                            Debug.Log("Entrando en estado curar");
                    }
                // If there is a wounded ally and I can "shoot" him in a straight line, "shoot" him
                npc.CambiarEstado(npc.estadoAtaqueRango, allyChoosen);
                return true;
            }
        }
        return false;
    }

    // Check whether to switch to melee or ranged attack
    protected bool ComprobarAtaqueRangoMelee(NPC npc) {
        if(npc.user)
            return false;
        List<NPC> enemies = UnitsManager.EnemigosEnRango(npc);
        if (enemies != null && enemies.Count > 0) {
            // There are close enemies
            if (npc.tipo == NPC.TipoUnidad.Brawler || npc.tipo == NPC.TipoUnidad.Medic) {
                // I have no ammo
                if (UnitsManager.EnemigosCerca(npc) - UnitsManager.AliadosCerca(npc) <= npc.maxEnemigosMelee - npc.minAliadosMelee) {
                    // I have enough support to fight
                    if (!npc.gameManager.InBase(npc) && !npc.gameManager.InBase(enemies[0])) {
                        Debug.Log("Entrando en estado ataque " + npc.name);
                        // If I am not inside the base and neither the enemy, attack said enemy
                        npc.CambiarEstado(npc.estadoAtaqueMelee, enemies[0]);
                        return true;
                    }
                }
            } else if (npc.tipo == NPC.TipoUnidad.Ranged) {
                // I do have ammo
                foreach (NPC en in enemies) {
                    float distance = Vector3.Distance(npc.agentNPC.Position, en.agentNPC.Position);
                    if (distance <= npc.rangedRange && !npc.gameManager.InBase(npc) && !npc.gameManager.InBase(en)) {
                        // If the enemy is within my range, I have a straight shooting line and neither of us are in base, shoot said enemy
                        npc.CambiarEstado(npc.estadoAtaqueRango, en);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    
    protected bool ComprobarUser(NPC npc) {
        if(npc.user){
            npc.CambiarEstado(npc.estadoUsuario);
            return true;
        }   
        return false;
    }

    // Get the name of the current state
    public override string ToString() {
        return GetType().Name;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nada : Estado
{
    public override void EntrarEstado(NPC npc) {

    }

    public override void SalirEstado(NPC npc) {

    }

    public override void Accion(NPC npc) {
        /*GameManager gameManager = npc.GameManager;
        
        // If the unit is dead, change to that state
        if (IsDead(npc))
            return;
        // If there are too many enemies, flee
        if (CheckEscape(npc))
            return;
        // Otherwise, if I am a medic, check if there are nearby allies to "shoot" (heal) and if so, shoot them
        if (CheckMedicRangedAttack(npc))
            return;
        // Otherwise, check first if I have to defend my capture point
        if (CheckDefend(gameManager, npc))
            return;
        // Otherwise, check if I can capture the enemy point
        if (CheckCapture(gameManager, npc))
            return;
        // Otherwise, check if I can attack any enemies
        if (CheckMeleeAndRangedAttack(npc))
            return;
            */

    }
/**
    public bool IsDead(NPC npc) {

        if (npc.Health == 0) {
            npc.ChangeState(npc.DeadState);
            return true;
        }
        return false;
    }


    public bool CheckDefend(GameManager gameManager, NPC npc) {
        if (gameManager.EnemiesAtCheckpoint(npc) > 0 && (npc.Health > npc.LowHealth || gameManager.TotalWarMode) && !gameManager.NPCInWaypoint(npc, gameManager.WaypointManager.GetAlliedCheckpoint(npc))) {
            // If there are enemies in our capture point and I have enough health or it's total war, go defend
            npc.ChangeState(npc.DefendState);
            return true;
        }
        return false;
    }

    public bool CheckCapture(GameManager gameManager, NPC npc) {
        if ((!npc.Patrol || npc.Patrol && gameManager.TotalWarMode) && UnitsManager.EnemiesNearby(npc) == 0 && npc.Health > npc.LowHealth) {
            // If I'm not supposed to patrol or it's total war mode and there are no nearby enemies and I have enough health
            if (gameManager.AlliesCapturing(npc) >= npc.MinAlliesForCapture) {
                // If there are enough allies capturing, go capture too
                npc.ChangeState(npc.CaptureState);
                return true;
            }
        }
        return false;
    }

    public bool CheckEscape(NPC npc) {
        // There is no running away in total war mode
        if (npc.GameManager.TotalWarMode)
            return false;
        
        if (npc.Health <= npc.LowHealth || UnitsManager.EnemiesNearby(npc) > npc.EnemiesToRun) {
            // If I have low health or there are too many enemies, flee
            npc.ChangeState(npc.EscapeState);
            return true;
        }
        return false;
    }

    public bool CheckMedicRangedAttack(NPC npc) {
        if (npc.Type == NPC.UnitType.Medic) {
            // If I am medic
            NPC allyChoosen = UnitsManager.ChooseAlly(npc);
            if (allyChoosen != null) {
                // If there is a wounded ally and I can "shoot" him in a straight line, "shoot" him
                npc.ChangeState(npc.RangedAttackState, allyChoosen);
                return true;
            }
        }
        return false;
    }

    // Check whether to switch to melee or ranged attack
    public bool CheckMeleeAndRangedAttack(NPC npc) {
        List<NPC> enemies = UnitsManager.EnemiesInRange(npc);
        if (enemies != null && enemies.Count > 0) {
            // There are close enemies
            if (npc.CurrentAmmo == 0) {
                // I have no ammo
                if (UnitsManager.EnemiesNearby(npc) - UnitsManager.AlliesNearby(npc) <= npc.MaxEnemiesForMelee - npc.MinAlliesForMelee) {
                    // I have enough support to fight
                    if (!npc.GameManager.InBase(npc) && !npc.GameManager.InBase(enemies[0])) {
                        // If I am not inside the base and neither the enemy, attack said enemy
                        npc.ChangeState(npc.MeleeAttackState, enemies[0]);
                        return true;
                    }
                }
            } else {
                // I do have ammo
                foreach (NPC en in enemies) {
                    float distance = Vector3.Distance(npc.AgentNPC.Position, en.AgentNPC.Position);
                    if (distance <= npc.RangedRange && !npc.GameManager.InBase(npc) && !npc.GameManager.InBase(en)) {
                        // If the enemy is within my range, I have a straight shooting line and neither of us are in base, shoot said enemy
                        npc.ChangeState(npc.RangedAttackState, en);
                        return true;
                    }
                }
            }
        }
        return false;
    }**/
}

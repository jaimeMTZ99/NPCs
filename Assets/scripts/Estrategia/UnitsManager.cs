using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using System.Linq;

public static class UnitsManager {

    private static int npcLayerMask = 1 << 9;

    // Defines the range for each of the OverlapShere calls used in this script
    // Essentially, it is the distance at which a unit can "see"
    private static int sphereRange = 25;

    // Returns the number of enemies near that the unit can see
    public static int EnemigosCerca(NPC npc) {
        int result = 0;
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, 5, npcLayerMask);
        int i = 0;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team != npc.team && !actualNPC.IsDead && DirectLine(npc, actualNPC))
                result++;
            i++;
        }
        return result;
    }

    // Returns the number of allies near that the unit can see
    public static int AliadosCerca(NPC npc) {
        int result = 0;
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, 5, npcLayerMask);
        int i = 0;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team == npc.team && !actualNPC.IsDead && DirectLine(npc, actualNPC))
                result++;
            i++;
        }
        return result;
    }
    
    // Returns all enemies within the visible range ordered by distance
    public static List<NPC> EnemigosEnRango(NPC npc) {
        List<NPC> enemies = new List<NPC>();
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, npc.rangedRange);
        int i = 0;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team != npc.team && DirectLine(npc, actualNPC) && !actualNPC.IsDead) {
                enemies.Add(actualNPC);
            }
            i++;
        }
        if (enemies.Count > 0) {
            enemies = enemies.OrderBy(e => Vector3.Distance(e.agentNPC.Position, npc.agentNPC.Position)).ToList();
        }
        if (npc.name == "MeleeFra 1" || npc.name == "MeleeEsp 2"){
            Debug.Log("Enemigos detectados del npc " + npc.name + " " + enemies.Count);
        }
        return enemies;
    }

    // Returns the closest medic alive to the unit
    public static NPC MedicoCerca(NPC npc) {
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, sphereRange, npcLayerMask);
        int i = 0;
        float minimalDistance = float.MaxValue;
        NPC selected = null;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC.team == npc.team && actualNPC.tipo == NPC.TipoUnidad.Medic && !actualNPC.IsDead) {
                float distance = Vector3.Distance(actualNPC.agentNPC.Position, npc.agentNPC.Position);
                if (distance < minimalDistance) {
                    minimalDistance = distance;
                    selected = actualNPC;
                }
            }
            i++;
        }
        return selected;
    }
    
    // Returns the closes ally, if any
    public static NPC AliadoCercano(NPC npc) {
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, sphereRange, npcLayerMask);
        int i = 0;
        float minimalDistance = float.MaxValue;
        NPC allySelected = null;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team == npc.team && !actualNPC.IsDead) {
                float distance = Vector3.Distance(actualNPC.agentNPC.Position, npc.agentNPC.Position);
                if (distance < minimalDistance) {
                    minimalDistance = distance;
                    allySelected = actualNPC;
                }  
            }
            i++;
        }

        if (allySelected != null)
            return allySelected;
        return null;
    }
    
    // Find the ally with the lowest health between all close units that are "LowHealth"
    public static NPC ElegirAliado(NPC npc) {
        List<NPC> lowHealth = new List<NPC>();
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, npc.rangedRange);
        int i = 0;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team == npc.team && DirectLine(npc, actualNPC) && !actualNPC.IsDead) {
                if (actualNPC.health <= actualNPC.maxVida/2)
                    lowHealth.Add(actualNPC);
            }
            i++;
        }
        if (lowHealth.Count > 0) {
            lowHealth = lowHealth.OrderBy(e => e.health).ToList();
            return lowHealth[0];
        }
        return null;
    }

    // Are there any obstacles between the attacker and the target?
    public static bool DirectLine(NPC attacker, NPC attacked) {
        RaycastHit hit;
        var direction = attacked.agentNPC.Position - attacker.agentNPC.Position;
        if (Physics.Raycast(attacker.agentNPC.Position, direction, out hit))
            return hit.collider.GetComponent<NPC>() == attacked;
        return false;
    }

}
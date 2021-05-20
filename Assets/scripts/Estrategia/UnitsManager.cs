using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using System.Linq;

public static class UnitsManager {

    private static int rango = 25;        //rango que se establece para detectar colisiones


    // numero de aliados cerca de un npc
    public static int AliadosCerca(NPC npc) {
        int resultado = 0;
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, 5);
        int i = 0;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team == npc.team && !actualNPC.IsDead && DirectLine(npc, actualNPC))
                resultado++;
            i++;
        }
        return resultado;
    }

    // comprobamos si hay algo en medio entre el atacante y el atacado
    public static bool DirectLine(NPC atacante, NPC atacado) {
        RaycastHit hit;
        var direction = atacado.agentNPC.Position - atacante.agentNPC.Position;
        if (Physics.Raycast(atacante.agentNPC.Position, direction, out hit))
            return hit.collider.GetComponent<NPC>() == atacado;
        return false;
    }

    // numero de enemigos que un NPC puede ver
    public static int EnemigosCerca(NPC npc) {
        int resultado = 0;
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, 5);
        int i = 0;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team != npc.team && !actualNPC.IsDead && DirectLine(npc, actualNPC))
                resultado++;
            i++;
        }
        return resultado;
    }

    // enemigos dentro del rango de un NPC
    public static List<NPC> EnemigosEnRango(NPC npc) {
        List<NPC> enemigos = new List<NPC>();
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, npc.rangedRange);
        int i = 0;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team != npc.team && DirectLine(npc, actualNPC) && !actualNPC.IsDead) {
                enemigos.Add(actualNPC);
            }
            i++;
        }
        if (enemigos.Count > 0) {
            enemigos = enemigos.OrderBy(e => Vector3.Distance(e.agentNPC.Position, npc.agentNPC.Position)).ToList();
        }
        return enemigos;
    }

    // devuelve quien es el agente medico mas cercano (posiblemente metamos mas medicos)
    public static NPC MedicoCerca(NPC npc) {
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, rango);
        int i = 0;
        float minDistancia = float.MaxValue;
        NPC seleccionados = null;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team == npc.team && actualNPC.tipo == NPC.TipoUnidad.Medic && !actualNPC.IsDead) {
                float distancia = Vector3.Distance(actualNPC.agentNPC.Position, npc.agentNPC.Position);
                if (distancia < minDistancia) {
                    minDistancia = distancia;
                    seleccionados = actualNPC;
                }
            }
            i++;
        }
        return seleccionados;
    }
    
    // devuelve al aliado mas cercano de un NPC
    public static NPC AliadoCercano(NPC npc) {
        Collider[] hitColliders = Physics.OverlapSphere(npc.agentNPC.Position, rango);
        int i = 0;
        float minDistancia = float.MaxValue;
        NPC aliado = null;
        while (i < hitColliders.Length) {
            NPC actualNPC = hitColliders[i].GetComponent<NPC>();
            if (actualNPC != null && actualNPC.team == npc.team && !actualNPC.IsDead) {
                float distancia = Vector3.Distance(actualNPC.agentNPC.Position, npc.agentNPC.Position);
                if (distancia < minDistancia) {
                    minDistancia = distancia;
                    aliado = actualNPC;
                }  
            }
            i++;
        }

        if (aliado != null)
            return aliado;
        return null;
    }
    
    //devuelve el aliado con menos vida
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

}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Grid gridMap;
    public List<NPC> npcs;
    public bool totalWarMode;

    public WaypointManager waypointManager;
    [SerializeField]

    private float minDistance = 3.5f;


    //GUI de ganar o restear el juego
    [SerializeField] private GameObject espanaGana;
    [SerializeField] private GameObject franciaGana;

    [SerializeField] private GameObject mapaInf;
    [SerializeField] private GameObject mapaVis;
    // Start is called before the first frame update
    void Start()
    {
        foreach (NPC npc in npcs) {
            npc.gameManager = this;
            npc.gridMap = gridMap;
        }
        waypointManager.gm = this;
        waypointManager.grid = gridMap;
       // strategyInputManager.GameManager = this;
    }
    
    void Update() {
        bool capturaFrancia = false;
        bool capturaEspana = false;
        foreach (NPC npc in npcs) {
            if (!npc.IsDead && NPCInWaypoint(npc, waypointManager.GetRival(npc))) {
                if (npc.team == NPC.Equipo.Spain)
                    capturaEspana = true;
                else
                    capturaFrancia = true;
            }
        }
        if (!capturaEspana) {
            waypointManager.EspCapturando();
        }
        if (!capturaFrancia) {
            waypointManager.FraCapturando();
        }
    }
    
    public int EnemigosCheckpoint(NPC npc) {
        int enemiesAtCheckpoint = 0;
        foreach (NPC npc2 in npcs) {
            if (npc2.team != npc.team && !npc2.IsDead) {
                foreach (Transform position in waypointManager.GetEquipo(npc).posiciones) {
                    if (Vector3.Distance(npc2.agentNPC.Position, position.position) <= minDistance)
                        enemiesAtCheckpoint++;
                }
            }
        }
        return enemiesAtCheckpoint;
    }

    public int AliadosCapturando(NPC npc) {
        int alliesCapturing = 0;
        foreach (NPC npc2 in npcs) {
            if (npc2.team == npc.team && !npc2.IsDead) {
                foreach (Transform position in waypointManager.GetRival(npc).posiciones) {
                    if (Vector3.Distance(npc2.agentNPC.Position, position.position) <= minDistance)
                        alliesCapturing++;
                }
            }
        }
        return alliesCapturing;
    }

    public bool EnemigosDefendiendo(NPC npc) {
        Waypoint enemyCheckpoint = waypointManager.GetRival(npc);
        foreach (NPC npc2 in npcs) {
            if(npc2.team != npc.team && !npc2.IsDead) {
                if (NPCInWaypoint(npc2, enemyCheckpoint))
                    return true;
            }
        }
        return false;
    }

    // Return true if the NPC is on base
    public bool InBase(NPC npc) {
        foreach (Transform position in waypointManager.GetBase(npc).posiciones) {
            if (Vector3.Distance(npc.agentNPC.Position, position.position) <= minDistance)
                return true;
        }
        return false;
    }

    // Return true if the NPC is on base
    public bool InCuracion(NPC npc) {
        foreach (Transform position in waypointManager.GetCuracion(npc).posiciones) {
            if (Vector3.Distance(npc.agentNPC.Position, position.position) <= minDistance){
                return true;
            }
        }
        return false;
    }

    public bool NPCInWaypoint(NPC npc, Waypoint waypoint) {
        foreach (Transform position in waypoint.posiciones) {
            if (Vector3.Distance(npc.agentNPC.Position, position.position) <= minDistance)
                return true;
        }
        return false;
    }

    public void CambiarModoOfensivo(NPC.Equipo team) {
        foreach (NPC npc in npcs)
            if (npc.team == team)
                npc.DispararModoOfensivo();
    }

    public void CambiarModoDefensivo(NPC.Equipo team) {
        foreach (NPC npc in npcs)
            if (npc.team == team)
                npc.DispararModoDefensivo();
    }

    public void ModoGuerraTotal() {
        totalWarMode = true;

        foreach (NPC npc in npcs)
            npc.DispararGuerraTotal();
    }

    public void FranciaGana() {
        franciaGana.SetActive(true);
        Time.timeScale = 0;
    }

    public void EspanaGana() {
        espanaGana.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void cambiarVista() {
        if(mapaInf.activeSelf){
            mapaInf.SetActive(false);
            mapaVis.SetActive(true);
        } else {
            mapaVis.SetActive(false);
            mapaInf.SetActive(true);
        }
    }
    public void Musica() {
        AudioSource audio = GetComponent<AudioSource>();

        if (audio.isPlaying)
            audio.Stop();
        else
            audio.Play();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{


    public Grid grid;

    public GameManager gm;

    [SerializeField]
    private Waypoint baseESP;
    [SerializeField]
    private Waypoint baseFRA;
    [SerializeField]
    private Waypoint zonaESP;
    [SerializeField]
    private Waypoint zonaFRA;
    [SerializeField]
    private Waypoint[] coberturas;


    public Nodo GetNodoAleatorio(Waypoint wp) {
        int random = Random.Range(0, wp.posiciones.Length);
        return grid.GetNodoPosicionGlobal(wp.posiciones[random].position);
    }

    //Devuelve waypoint de la base del equipo
    public Waypoint GetBase(NPC npc) {
        if (npc.team == NPC.Equipo.France)
            return baseFRA;
        return baseESP;
    }

    //Devuelve waypoint de la zona del equipo
    public Waypoint GetEquipo(NPC npc) {
        if (npc.team == NPC.Equipo.France)
            return zonaFRA;
        return zonaESP;
    }

    //Devuelve waypoint de la zona del equipo rival
    public Waypoint GetRival(NPC npc) {
        if (npc.team == NPC.Equipo.France)
            return zonaESP;
        return zonaFRA;
    }

    //Devuelve el punto de cobertura más cercano
    public Nodo GetCobertura(NPC npc) {
        float minDist = float.MaxValue;
        Vector3 coberturaCercana = Vector3.zero;
        foreach (Waypoint cobertura in coberturas) {
            float distancia = Vector3.Distance(npc.GetComponent<AgentNPC>().transform.position, cobertura.posicion);
            if (distancia < minDist) {
                minDist = distancia;
                coberturaCercana = cobertura.posicion;
            }
        }
        return grid.GetNodoPosicionGlobal(coberturaCercana);
    }

    public void Captura(NPC npc) {
        if (npc.team == NPC.Equipo.France) {
            zonaESP.porcentajeCaptura *= Time.deltaTime;
            if (zonaESP.porcentajeCaptura >= 100);
                //gm.BlueWins();
        } else {
            zonaFRA.porcentajeCaptura *= Time.deltaTime;
            if (zonaESP.porcentajeCaptura >= 100);
                //gm.RedWins();
        }
    }


    //ESTO QUE? HAY QUE VER CONDICION VICTORIA
    /*public void RedTeamNotCapturing() {
        if (_blueCheckpointWaypoint.porcentajeCaptura > 0)
            _blueCheckpointWaypoint.porcentajeCaptura -= 0.5f * Time.deltaTime;
    }

    public void BluTeamNotCapturing() {
        if (_redCheckpointWaypoint.porcentajeCaptura > 0)
            _redCheckpointWaypoint.porcentajeCaptura -= 0.5f * Time.deltaTime;
    }

    public void Restart() {
        _blueCheckpointWaypoint.CapturePercentage = 0;
        _redCheckpointWaypoint.CapturePercentage = 0;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terreno
{
    /**
    public enum Tipo {
        Carretera,
        Bosque,
        Desierto,
        FranceBase,
        SpainBase,
        Rio
    }

    public int x;
    public int y;
    public int z;


    public Vector3 posicion;
    public Tipo terreno;

    public bool atravesable;


    public bool seeThrough;  
    public float influence;
    public float visibility;


    public float Coste(NPC.UnitType unitType, NPC.UnitTeam team) {
        if (!atravesable)
            return float.MaxValue;
        
        switch (Tipo) {
            case Tipo.Bosque:
                switch (unitType) {
                    case NPC.UnitType.Soldado:
                        return 1f; 
                    case NPC.UnitType.Tanque: 
                        return 1.66f;
                    case NPC.UnitType.Medico:
                        return 1;
                }
                break;
            case Tipo.Desierto:
                switch (unitType) {
                    case NPC.UnitType.Soldado:
                        return 1.33f; 
                    case NPC.UnitType.Tanque:
                        return 2f;
                    case NPC.UnitType.Medico:
                        return 1.33f;

                }
                break;
            case Tipo.Carretera:
                switch (unitType) {
                    case NPC.UnitType.Soldado:
                        return 0.7f; 
                    case NPC.UnitType.Tanque:
                        return 0.6f;
                    case NPC.UnitType.Medico:
                        return 0.7f;

                }
                break;
            case Tipo.FranceBase:
                switch (team) {
                    case NPC.UnitTeam.Red:
                        return 1000;
                    case NPC.UnitTeam.Blu:
                        return 1;
                }
                break;
            case Tipo.SpainBase:
                switch (team) {
                    case NPC.UnitTeam.Red:
                        return 1;
                    case NPC.UnitTeam.Blu:
                        return 1000;
                }
                break;
            case Tipo.Rio:
                return float.MaxValue;
            default:
                return 1;
        }
        return 1;
    }

    // Returns the multiplying factor that each individual unit should apply while traversing this tile
    public float SpeedMultiplier(NPC.UnitType unitType) {
        switch (Tipo) {
            case Tipo.Bosque:
                switch (unitType) {
                    case NPC.UnitType.Soldado:
                        return 1f; 
                    case NPC.UnitType.Tanque: 
                        return 0.75f;
                    case NPC.UnitType.Medico:
                        return 1f;

                }
                break;
            case Tipo.Desierto:
                switch (unitType) {
                    case NPC.UnitType.Soldado:
                        return 0.8f; 
                    case NPC.UnitType.Tanque:
                        return 0.5f;
                    case NPC.UnitType.Medico:
                        return 0.7f;

                }
                break;
            case Tipo.Carretera:
                switch (unitType) {
                    case NPC.UnitType.Soldado:
                        return 1.2f; 
                    case NPC.UnitType.Tanque:
                        return 1.5f;
                    case NPC.UnitType.Medico:
                        return 1.1f;

                }
                break;
            case Tipo.FranceBase:
                switch (team) {
                    case NPC.UnitTeam.Red:
                        return 1;
                    case NPC.UnitTeam.Blu:
                        return 1.5f;
                }
                break;
            case Tipo.SpainBase:
                switch (team) {
                    case NPC.UnitTeam.Red:
                        return 1.5f;
                    case NPC.UnitTeam.Blu:
                        return 1;
                }
                break;
            default:
                return 1;
        }
        return 1;
    }
**/

}

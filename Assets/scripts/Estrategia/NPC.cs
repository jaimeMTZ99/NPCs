using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum TipoUnidad{
        Brawler,
        Ranged,
        Medic
    }

    public enum Equipo{
        Spain,
        France
    }

    public TipoUnidad tipo;
    public Equipo team;

    public int health;

    public int meleeDamage;
    public int meleeDamageCrit;
    public float rangeMelee;
    public float meleeAttackSpeed;

    public int rangedDamage;
    public int rangedDamageCrit;
    public float rangeRanged;
    public float rangedAttackSpeed;

    public float speed;

    public int captureRatio;
}

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

    private Grid gridMap;
    private GameManager gameManager;
    public AgentNPC _agentNPC;
    //private SimplePropagator _simplePropagator;
    public PathFinding pf;


    //info de la unidad
    public TipoUnidad tipo;
    public Equipo team;


    //caracteristicas de la unidad
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


    //Pesos segun los modos

    public int aliadosAtacantes;
    public float menosVida;
    public float masVida;
    public int aliadosDefendiendo;
    public int capturar;


        // Estados
        /**
    [SerializeField] private State _currentState;
    private Capture _captureState;
    private Defend _defendState;
    private Escape _escapeState;
    private Heal _healState;
    private Idle _idleState;
    private MeleeAttack _meleeAttackState;
    private Patrol _patrolState;
    private RangedAttack _rangedAttackState;
    private Dead _deadState; **/
    

}

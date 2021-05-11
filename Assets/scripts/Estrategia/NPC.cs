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
    //######REFERENCIAS A OBJETOS#########
    public Grid gridMap;
    public GameManager gameManager;
    public AgentNPC agentNPC;
    //private SimplePropagator _simplePropagator;
    public PathFinding pf;


    //##########INFO DE LA UNIDAD###########
    public TipoUnidad tipo;
    public Equipo team;
    public bool patrol;
    public Vector3 startPosition;

    public Nodo nodoActual;

    //#########CARACTERISTICAS DE LA UNIDAD###########
    public float health;
    public int meleeDamage;
    public int meleeDamageCrit;
    public float rangoMelee;
    public float meleeAttackSpeed;

    public int rangedDamage;
    public int rangedDamageCrit;
    public float rangedRange;
    public float rangedAttackSpeed;
    public int municionActual;
    
    public int municionPorTiro;
    public int tiempoCargaRango;
    //#############Pesos segun los modos#############
    public int aliadosAtacantes;
    public float menosVida;
    public float maxVida;
    public int aliadosDefendiendo;
    public int capturar;
    public int numEnemigosEscape;
    public int minAliadosCaptura;
    public int maxEnemigosMelee;
    public int minEnemigosMelee;
    public int maxMunicion;

    //######### Estados###########
        
    [SerializeField] 
    private Estado currentState;
    public Captura estadoCaptura;
    public Defender estadoDefensa;
    public Escapar estadoEscapar;
   // private Heal _healState;
    public AsignarEstado estadoAsignado;
    public AtaqueMelee estadoAtaqueMelee;
   // private Patrol _patrolState;
    public AtaqueRango estadoAtaqueRango;
    public Muerto estadoMuerto; 
    public bool IsDead => currentState == estadoMuerto;
    //Función que sirve para cambiar el estado del NPC
    public void CambiarEstado(Estado newState, NPC objective = null) {

        if (currentState != null && currentState != newState)
            currentState.SalirEstado(this);

        newState.SetObjective(objective);

        if (currentState != newState) {
            currentState = newState;
            //GUIManager.TriggerAnimation(statusAnimator);
            currentState.EntrarEstado(this);
        }
    }

        public void DispararModoOfensivo() {
        /*_enemiesToRun = _initialEnemiesToRun + 1;
        _lowHealth = _initialLowHealth - 10;
        _healthy = _initialHealthy - 20;
        _maxEnemiesForMelee = _initialMaxEnemiesForMelee + 1;
        _minAlliesForCapture = 0;
        if (_minAlliesForMelee > 0)
            _minAlliesForMelee = _initialMinAlliesForMelee - 1;

        // Pathfinding
        _pathfinding.TerrainCostMultiplier = 1;
        _pathfinding.InfluenceCostMultiplier = 1;
        _pathfinding.VisibilityCostMultiplier = 2;*/
    }

    public void DispararModoDefensivo() {
        /*_enemiesToRun = _initialEnemiesToRun - 1;
        _lowHealth = _initialLowHealth + 20;
        _healthy = _initialHealthy + 10;
        _minAlliesForCapture = _initialAlliesForCapture;
        if (_maxEnemiesForMelee > 0)
            _maxEnemiesForMelee = _initialMaxEnemiesForMelee - 1;
        _minAlliesForMelee = _initialMinAlliesForMelee + 1;

        // Pathfinding
        _pathfinding.TerrainCostMultiplier = 1;
        _pathfinding.InfluenceCostMultiplier = 2;
        _pathfinding.VisibilityCostMultiplier = 1;*/
    }

    public void DispararGuerraTotal() {
        /*_enemiesToRun = _initialEnemiesToRun + 2;
        _lowHealth = _initialLowHealth - 20;
        _healthy = _initialHealthy - 30;
        _maxEnemiesForMelee = _initialMaxEnemiesForMelee + 2;
        _minAlliesForCapture = 0;
        if (_minAlliesForMelee > 0)
            _minAlliesForMelee = _initialMinAlliesForMelee - 1;

        // Pathfinding
        _pathfinding.TerrainCostMultiplier = 1;
        _pathfinding.InfluenceCostMultiplier = 0;
        _pathfinding.VisibilityCostMultiplier = 0;*/
    }
    public void ActualizarIconoEstado() {
        //GUIManager.UpdateStateIcon(_status, _currentState);
    }
     public void AñadirAlGrupo(float speed, TipoUnidad type) {
        /*groupSpeed = speed;
        pathfinding.Type = type;*/
    }

    public void QuitarDelGrupo() {
        /*groupSpeed = speed;
        pathfinding.Type = unitType;*/
    }

    public void Restart() {
        health = maxVida;
        municionActual = maxMunicion;
        QuitarDelGrupo();
        this.GetComponent<Path>().ClearPath();
        agentNPC.Position = startPosition;
        //Idle estado?
        //CambiarEstado(_idleState);
    }

}

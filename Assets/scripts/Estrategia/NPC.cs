using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum TipoUnidad
    {
        Brawler,
        Ranged,
        Medic
    }

    public enum Equipo
    {
        Spain,
        France
    }

    //######REFERENCIAS A OBJETOS#########
    public Grid gridMap;
    public GameManager gameManager;
    public AgentNPC agentNPC;
    public SimplePropagator simplePropagator;
    public PathFinding pf;


    //##########INFO DE LA UNIDAD###########
    public TipoUnidad tipo;
    public Equipo team;
    public Vector3 startPosition;

    public Nodo nodoActual;
    public Nodo anteriorNodo;
    public int radio = 1;
    public float influencia = 1f;
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
    public float healthy;
    //#############Pesos segun los modos#############
    public int aliadosAtacantes;
    public float menosVida;
    public float maxVida;
    public int aliadosDefendiendo;
    public int capturar;
    public int numEnemigosEscape;
    public int minAliadosCaptura;
    public int maxEnemigosMelee;
    public int minAliadosMelee;
    //############ PATROL ###############
    public bool patrol;
    public Transform puntoPatrullaInicial;
    public Transform puntoPatrullaFin;
    //######### Estados###########

    [SerializeField]
    private Estado currentState;
    public Captura estadoCaptura;
    public Defender estadoDefensa;
    public Escapar estadoEscapar;
    public Curar estadoCuracion;
    public AsignarEstado estadoAsignado;
    public AtaqueMelee estadoAtaqueMelee;
    public Patrullar estadoPatrullar;
    public AtaqueRango estadoAtaqueRango;
    public Muerto estadoMuerto;
    public Vagar estadoVagar;
    public bool IsDead => currentState == estadoMuerto;
    //Función que sirve para cambiar el estado del NPC
    void Start()
    {
        /*if (pathfinding)
        {
            pathfinding.Type = _unitType;
            pathfinding.Team = _team;
        }*/
        agentNPC = GetComponent<AgentNPC>();
        simplePropagator = GetComponent<SimplePropagator>();
        Initialize();
    }
    protected void Initialize() {
        currentState = null;
        estadoCaptura = new Captura();
        estadoDefensa = new Defender();
        estadoEscapar = new Escapar();
        estadoCuracion = new Curar();
        estadoAsignado = new AsignarEstado();
        estadoAtaqueMelee = new AtaqueMelee();
        estadoPatrullar = new Patrullar();
        estadoAtaqueRango = new AtaqueRango();
        estadoMuerto = new Muerto();
        //_userState = new User();
        estadoVagar = new Vagar();
        health = maxVida;
        //_currentHealthAnimation = _maxHealth;
        //_groupSpeed = _speed;
        startPosition = agentNPC.Position;
        CambiarEstado(estadoAsignado);
    }


    void Update()
    {
        if (currentState != null){
            currentState.Ejecutar(this);
        }

        // Most likely not the best way to do this
        agentNPC.maxSpeed =  gridMap.GetNodoPosicionGlobal(agentNPC.Position).SpeedMultiplier(tipo);
         nodoActual = gridMap.GetNodoPosicionGlobal(agentNPC.Position);

        if (nodoActual.walkable)
            anteriorNodo = nodoActual;
    }

    public void CambiarEstado(Estado newState, NPC objective = null)
    {
        if (currentState != null && currentState != newState)
            currentState.SalirEstado(this);

        newState.SetObjective(objective);
        if (currentState == null || currentState != newState)
        {
            currentState = newState;
            //GUIManager.TriggerAnimation(statusAnimator);
            currentState.EntrarEstado(this);
        }
    }

    public void DispararModoOfensivo()
    {
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

    public void DispararModoDefensivo()
    {
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

    public void DispararGuerraTotal()
    {
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
    public void ActualizarIconoEstado()
    {
        //GUIManager.UpdateStateIcon(_status, _currentState);
    }
    public void AñadirAlGrupo(float speed, TipoUnidad type)
    {
        /*groupSpeed = speed;
        pathfinding.Type = type;*/
    }

    public void QuitarDelGrupo()
    {
        /*groupSpeed = speed;
        pathfinding.Type = unitType;*/
    }
    public virtual float GetDropOff()
    {
        return influencia;
    }
    public void Restart()
    {
        health = maxVida;
        QuitarDelGrupo();
        this.GetComponent<Path>().ClearPath();
        agentNPC.Position = startPosition;
        CambiarEstado(estadoAsignado);
    }

}

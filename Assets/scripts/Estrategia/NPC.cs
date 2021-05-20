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

    public bool user;

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
    public int enemigosEscapeBase;
    public int enemigosMeleeBase;
    public int aliadosCapturaBase;
    public int minAliadosMeleeBase;
    public int menosVidaBase;
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
    public User estadoUsuario;
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

        estadoUsuario = new User();
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
            Debug.Log(currentState + this.name);
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
       numEnemigosEscape = enemigosEscapeBase + 1;
        menosVida = menosVidaBase - 50;
        maxEnemigosMelee = enemigosMeleeBase + 1;
        minAliadosCaptura = 0;
        if (minAliadosMelee > 0)
            minAliadosMelee = minAliadosMeleeBase - 1;
    }

    public void DispararModoDefensivo()
    {
        numEnemigosEscape = enemigosEscapeBase - 1;
        menosVida = menosVidaBase - 50;
        minAliadosCaptura = aliadosCapturaBase;
        if (maxEnemigosMelee > 0)
            maxEnemigosMelee = enemigosMeleeBase - 1;
        minAliadosMelee = minAliadosMeleeBase + 1;
    }

    public void DispararGuerraTotal()
    {
        numEnemigosEscape = enemigosEscapeBase + 2;
        menosVida = menosVidaBase - 50;
        maxEnemigosMelee = enemigosMeleeBase + 2;
        minAliadosCaptura = 0;
        if(minAliadosMelee > 0){
            minAliadosMelee = minAliadosMeleeBase - 1;
        }
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

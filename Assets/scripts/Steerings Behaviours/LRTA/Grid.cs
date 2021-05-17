using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Transform inicio; //Inicio del pathfinding
    public Vector2 tamGrid; //Tamaño del grid (unidades reales)
    public float radioNodo;
    public float distanciaNodos;

    public Nodo[,] Nodos; //Nodos
    Transform[,] mapa;
    public GameObject cubos;
    public int mapaFila;
    public int mapaColumna;
    private Transform fila;
    private Transform columna;
    public List<Nodo> camino;
    float diametroNodo;
    int tamGridX, tamGridY; //Tamaño del grid en relacion a los nodos

    public InfluenceMapControl mapaInfluencia;
    public Transform abajoIzq;
    public Transform arribaDcha;
    private void Awake()
    {
        mapa = new Transform[mapaFila, mapaColumna];
        for (int i = 0; i < mapaFila; i++)
        {
            fila = cubos.transform.GetChild(i);
            for (int y = 0; y < mapaColumna; y++)
            {
                columna = fila.transform.GetChild(y);
                mapa[y, i] = columna;
            }
        }
    }


    private void Start()
    {
        diametroNodo = radioNodo * 2;
        tamGridX = Mathf.RoundToInt(tamGrid.x / diametroNodo);
        tamGridY = Mathf.RoundToInt(tamGrid.y / diametroNodo);
        CrearGrid();
        if (mapaInfluencia != null){
             float minX, maxX, minZ, maxZ;

        // x
        if (abajoIzq.position.x < arribaDcha.position.x) {
            minX = abajoIzq.position.x;
            maxX = arribaDcha.position.x;
        } else {
            maxX = abajoIzq.position.x;
            minX = arribaDcha.position.x;
        }  
        
        // z
        if (abajoIzq.position.z < arribaDcha.position.z) {
            minZ = abajoIzq.position.z;
            maxZ = arribaDcha.position.z;
        } else {
            maxZ = abajoIzq.position.z;
            minZ = arribaDcha.position.z;
        }  

        int x = Mathf.RoundToInt((maxX - minX) / radioNodo*2) + 1;
        int z = Mathf.RoundToInt((maxZ - minZ) / radioNodo*2) + 1;
            mapaInfluencia.Initialize(x,z);
        }
    }

    //Inicializa el grid
    void CrearGrid()
    {
        int[,] matrizCostes = ObtenerMatrizCostes();
        Nodos = new Nodo[tamGridX, tamGridY];
        Vector3 esquina = transform.position - Vector3.right * tamGrid.x / 2 - Vector3.forward * tamGrid.y / 2;
        for (int x = 0; x < tamGridX; x++)
        {
            for (int y = 0; y < tamGridY; y++)
            {
                Vector3 worldPoint = esquina + Vector3.right * (x * diametroNodo + radioNodo) + Vector3.forward * (y * diametroNodo + radioNodo);
                bool walkable = !isObjectHere(worldPoint);
                Nodos[x, y] = new Nodo(walkable, worldPoint, x, y);//Create a new node in the array.
                Nodos[x,y].igCost = matrizCostes[x,y];
            }
        }
    }

    //Obtiene los nodos vecinos al dado
    public List<Nodo> GetVecinos(Nodo z)
    {
        List<Nodo> vecinos = new List<Nodo>();
        int icheckX;
        int icheckY;

        icheckX = z.X + 1;
        icheckY = z.Y;
        if (icheckX >= 0 && icheckX < tamGridX)
        {
            if (icheckY >= 0 && icheckY < tamGridY)
            {
                vecinos.Add(Nodos[icheckX, icheckY]);
            }
        }

        icheckX = z.X - 1;
        icheckY = z.Y;
        if (icheckX >= 0 && icheckX < tamGridX)
        {
            if (icheckY >= 0 && icheckY < tamGridY)
            {
                vecinos.Add(Nodos[icheckX, icheckY]);
            }
        }

        icheckX = z.X;
        icheckY = z.Y + 1;
        if (icheckX >= 0 && icheckX < tamGridX)
        {
            if (icheckY >= 0 && icheckY < tamGridY)
            {
                vecinos.Add(Nodos[icheckX, icheckY]);
            }
        }

        icheckX = z.X;
        icheckY = z.Y - 1;
        if (icheckX >= 0 && icheckX < tamGridX)
        {
            if (icheckY >= 0 && icheckY < tamGridY)
            {
                vecinos.Add(Nodos[icheckX, icheckY]);
            }
        }

        return vecinos;
    }


    //Obtiene el nodo correspondiente a las coordenadas reales
    public Nodo GetNodoPosicionGlobal(Vector3 a_vWorldPos)
    {
        float ixPos = ((a_vWorldPos.x + tamGrid.x / 2) / tamGrid.x);
        float iyPos = ((a_vWorldPos.z + tamGrid.y / 2) / tamGrid.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((tamGridX - 1) * ixPos);
        int iy = Mathf.RoundToInt((tamGridY - 1) * iyPos);
        if (Nodos != null && ix < mapaFila && ix >= 0 &&  iy < mapaColumna && iy >= 0)
            return Nodos[ix, iy];
        return null;
    }
    public Vector3 GetIndicesNodos(Vector3 a_vWorldPos)
    {
        float ixPos = ((a_vWorldPos.x + tamGrid.x / 2) / tamGrid.x);
        float iyPos = ((a_vWorldPos.z + tamGrid.y / 2) / tamGrid.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((tamGridX - 1) * ixPos);
        int iy = Mathf.RoundToInt((tamGridY - 1) * iyPos);
        if (Nodos != null && ix < mapaFila && ix >= 0 &&  iy < mapaColumna && iy >= 0)
            return new Vector3(ix,0,iy);
        return Vector3.zero;
    }

    //Obtiene el coste de un nodo
    int costeNodo(Transform nodo)
    {
        if(isObjectHere(nodo.position)){
            return 99999;
        }
        return 1;
    }

    //Obtiene la matriz de costes del grids
    public int[,] ObtenerMatrizCostes()
    {
        int[,] mapaCostes = new int[mapaFila, mapaColumna];
        for (int i = 0; i < mapaFila; i++)
        {
            for (int y = 0; y < mapaColumna; y++)
            {
                mapaCostes[i, y] = costeNodo(mapa[i, y]);
            }
        }
        return mapaCostes;
    }

    //Comprueba si hay un objeto que impide el paso    
    bool isObjectHere(Vector3 position)
    {
        Collider[] intersecting = Physics.OverlapSphere(position, radioNodo);
        foreach (Collider i in intersecting){
            if(i.gameObject.tag == "Muro" || i.gameObject.tag =="Agua"){

                return true;
            }
        }
        return false;
    }
}
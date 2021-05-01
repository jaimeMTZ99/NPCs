using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour{
    Nodo nodoActual;
    Nodo nodoFinal;
    GameObject nodoEnd; //Objeto visual
    LRTAStar lrtaStar = new LRTAStar();
    [SerializeField]
    private Grid grid;
    float[,] mapaCostes;
    /*private void Awake()
    {
        grid = grid.GetComponent<Grid>();
        
    }*/
    private void Start()
    {
        grid = grid.GetComponent<Grid>();
        Debug.Log(grid);
        mapaCostes = grid.ObtenerMatrizCostes("TUPUTAMADRE");
    }


    // Update is called once per frame
    void Update () {
       
    }

    public List<GameObject> EstablecerNodoFinal(AgentNPC agent)
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        List<Nodo> nodos;
        List<GameObject> keyPoints = new List<GameObject>();
        this.nodoActual = grid.NodeFromWorldPoint(agent.transform.position);
        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            if (hit.transform != null && hit.transform.tag != "Muro" && hit.transform.tag != "Agua")
            {
                if (nodoEnd != null) Destroy(nodoEnd);
                nodoEnd = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                nodoEnd.transform.localScale = new Vector3(1, 1, 1);
                nodoEnd.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                nodoEnd.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                nodoFinal = grid.NodeFromWorldPoint(nodoEnd.transform.position);
                Debug.Log("Nodo inicial" + nodoActual.iGridX + nodoActual.iGridY +  "  " + " Nodo final " + nodoFinal.iGridX + nodoFinal.iGridY);
                Debug.Log("Nodo inicial: " + agent.transform.position + " Nodo final: " + nodoEnd.transform.position );
                nodos = lrtaStar.FindPath(nodoActual, nodoFinal, 1/*npc.distancePathfinding*/, grid, mapaCostes);
                List<Vector3> aux = new List<Vector3>(nodos.Count);
                for (int i=0; i < nodos.Count; i++)
                {
                    GameObject keyPoint = new GameObject("Keypoint");
                    keyPoint.transform.position = nodos[i].vPosition;
                    keyPoints.Add(keyPoint);
                    
                }
                return keyPoints;

            }
        }

       return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    Nodo nodoActual;
    Nodo nodoFinal;

    [SerializeField]
    private int heuristica = 1;
    GameObject nodoEnd; //Objeto visual
    LRTA lrta = new LRTA();
    [SerializeField]
    public Grid grid;
    float[,] mapaCostes;
    private void Start()
    {
        grid = grid.GetComponent<Grid>();
    }

    //Establece el objetivo
    public List<GameObject> EstablecerNodoFinal(AgentNPC agent)
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        List<Nodo> nodos;
        List<GameObject> keyPoints = new List<GameObject>();
        this.nodoActual = grid.GetNodoPosicionGlobal(agent.transform.position);
        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            if (hit.transform != null && hit.transform.tag != "Muro")
            {
                if (nodoEnd != null) Destroy(nodoEnd);
                nodoEnd = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                nodoEnd.transform.localScale = new Vector3(grid.radioNodo*2, grid.radioNodo*2, grid.radioNodo*2);
                nodoEnd.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                nodoEnd.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                nodoFinal = grid.GetNodoPosicionGlobal(nodoEnd.transform.position);
                nodos = lrta.EncontrarCamino(nodoActual, nodoFinal, heuristica, grid);
                List<Vector3> aux = new List<Vector3>(nodos.Count);
                for (int i = 0; i < nodos.Count; i++)
                {
                    GameObject keyPoint = new GameObject("Keypoint");
                    keyPoint.transform.position = nodos[i].Posicion;
                    keyPoints.Add(keyPoint);

                }
                return keyPoints;

            }
        }

        return null;
    }

    //Establece objetivo respecto al lider de una formacion
    public List<GameObject> nodoFinalFormaciones(AgentNPC agent, GameObject esferaDestino)
    {
        List<Nodo> nodos;
        List<GameObject> keyPoints = new List<GameObject>();
        this.nodoActual = grid.GetNodoPosicionGlobal(agent.transform.position);
        Collider[] colliders = Physics.OverlapSphere(esferaDestino.transform.position, grid.radioNodo);
        bool muro = false;
        foreach (Collider c in colliders)
        {
            if (c.tag == "Muro")
            {
                muro = true;
            }
        }
        if (esferaDestino != null && !muro)
        {
            nodoFinal = grid.GetNodoPosicionGlobal(esferaDestino.transform.position);
            nodos = lrta.EncontrarCamino(nodoActual, nodoFinal, 1, grid);
            if (nodos != null)
            {
                List<Vector3> aux = new List<Vector3>(nodos.Count);
                for (int i = 0; i < nodos.Count; i++)
                {
                    GameObject keyPoint = new GameObject("Keypoint");
                    keyPoint.transform.position = nodos[i].Posicion;
                    keyPoints.Add(keyPoint);

                }
                return keyPoints;
            }

        }
        return null;
    }
}
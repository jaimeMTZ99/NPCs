using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFixed : MonoBehaviour
{
    //Tamaño del grid fijo
    private int tamañoGrid = 9;
    //Grid de posiciones relativas de los agentes.
    [SerializeField]
    private Vector3[] grid;
    //Agentes invisibles posicionados en el grid.
    private GameObject[] invisibles;
    //Punto de movimiento.
    private GameObject puntoDestinoGO;
    //Agentes que forman parte del grid
    [SerializeField]
    private List<AgentNPC> agentes;
    // Start is called before the first frame update
    void Start()
    {
        //agentes = new List<AgentNPC>();
        //grid = new Vector3[tamañoGrid];
        invisibles = new GameObject[tamañoGrid];
        puntoDestinoGO = new GameObject("punto destino");
        puntoDestinoGO.AddComponent<Agent>();

        int i = 0;
        foreach (AgentNPC ag in agentes)
        {
            GameObject invisibleGO = new GameObject("FC " + agentes.Count);
            Agent invisible = invisibleGO.AddComponent<Agent>() as Agent;
            invisibles[i] = invisibleGO;
            invisible.extRadius = 2f;
            invisible.intRadius = 2f;
            ag.form = true;
            i++;
            /*face = ag.GetComponent<Face>();
            ag.SteeringList.Remove(face);*/
        }
        UpdateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (agentes[0].llegar){
            Agent puntoDestinoInv = puntoDestinoGO.GetComponent<Agent>();
            puntoDestinoInv.transform.position = agentes[0].GetComponent<ArriveAcceleration>().target.transform.position;
        }
        UpdateSlots();
    }
    public void UpdateSlots() {
        
        for (int i = 0; i < agentes.Count; i++) {
            Vector3 pos = GetPosition(i);
            GameObject invisibleGOActual = invisibles[i];
            Agent invisibleActual = invisibleGOActual.GetComponent<Agent>();
            invisibleActual.transform.position =pos;
            if (i != 0){
                agentes[i].GetComponent<ArriveAcceleration>().target = invisibleActual;
                agentes[i].GetComponent<Face>().aux = invisibleActual;
                agentes[i].GetComponent<Face>().target = invisibleActual;
            }
            if (i == 0){
                agentes[0].GetComponent<Face>().aux = puntoDestinoGO.GetComponent<Agent>();
                agentes[0].GetComponent<Face>().target = puntoDestinoGO.GetComponent<Agent>();
            }
            
        }
    }
    // calcula la posicion
    public Vector3 GetPosition(int numero) {
        Vector3 agenteActual = grid[numero];
        AgentNPC lider = agentes[0];
        float distancia = 5;
        //distancia = (agentes[numero].transform.position - lider.transform.position).magnitude;
        /*if (diagonal){

        }
        else{
            
        }*/

        float[] matrizRotacion = new float[4]{Mathf.Cos(lider.orientation),-Mathf.Sin(lider.orientation),
                                            Mathf.Sin(lider.orientation), Mathf.Cos(lider.orientation)};
        
        Vector3 pm = productoMatricial(matrizRotacion, grid[numero]);
        Debug.Log(pm);
        Vector3 resultado = lider.transform.position + grid[numero] * distancia + pm;
        return resultado;
    }

    public Vector3 productoMatricial(float[] x,Vector3 posicionGrid ){
        Vector3 resultado = new Vector3(x[0] * posicionGrid.x + x[1] *posicionGrid.z,0, x[2] * posicionGrid.x + x[3]*posicionGrid.z);
        return resultado;
    }
}

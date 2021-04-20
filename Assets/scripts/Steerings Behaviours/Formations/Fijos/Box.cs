using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    [SerializeField]
    private float radio;

    private float ranuras = 4;
    //lista de los agentes seleccionados
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();

    private List<AgentNPC> asignaciones;

    void Start() {
        asignaciones = new List<AgentNPC>();

        //metemos los agentes que podemos para la formacion
        foreach (AgentNPC a in agentes) {
            if (asignaciones.Count<ranuras){
                asignaciones.Add(a);
            }
        }
        UpdateSlots();
    }
    void Update(){
        UpdateSlots();
    }
    public void UpdateSlots() {

        AgentNPC lider = asignaciones[0];

        lider.orientation *= -1; 
        
        for (int i = 0; i < asignaciones.Count; i++) {
            Vector3 pos = GetPosition(i);
            float ori = GetOrientation(i);
            // Transform by the anchor point position and orientation.
            var result = new Vector3(Mathf.Cos(lider.orientation) * pos.x -  Mathf.Sin(lider.orientation) * pos.z,
                0,
                Mathf.Sin(lider.orientation) * pos.x + Mathf.Cos(lider.orientation) * pos.z);

            // Set the position with seek
            GameObject ForC = new GameObject("FC");
            Agent invisible = ForC.AddComponent<Agent>() as Agent;
            invisible.extRadius=0.5f;
            invisible.intRadius=0.5f;
            invisible.transform.position =lider.transform.position + result;
            invisible.orientation =-(ori);
            asignaciones[i].GetComponent<SeekAcceleration>().target = invisible;
            // Set the orientation
            asignaciones[i].GetComponent<Align>().target = invisible;
        }
    }

        // calcula la orientacion
    public float GetOrientation(int numero) {

        float resultado;
        if (numero == 0) {
            resultado = - (Mathf.PI/2 + Mathf.PI);;
        }
        else if (numero == 1) {
            resultado = Mathf.PI / 2 + Mathf.PI;
        }
        else if (numero == 2) {
            resultado = - (Mathf.PI/4 + Mathf.PI/2);
        }
        else {
            resultado =  Mathf.PI / 4 + Mathf.PI / 2;
        }

        return resultado;
    }

    // calcula la posicion
    public Vector3 GetPosition(int numero) {

        Vector3 resultado;
        if (numero == 0) {
            resultado = Vector3.zero;
        }
        else if (numero == 1) {
            resultado = new Vector3(radio, 0, 0);
        }
        else if (numero == 2) {
            resultado = new Vector3(radio, 0, -radio);
        }
        else {
            resultado = new Vector3(0, 0, -radio);
        }

        return resultado;
    }

}
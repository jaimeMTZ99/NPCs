using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEsc : MonoBehaviour
{
    [SerializeField]
    private float radio;

    [SerializeField]
    private float ranuras;
    //lista de los agentes seleccionados
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();

    private List<AgentNPC> asignaciones;
    private GameObject centro;
    void Start() {
        asignaciones = new List<AgentNPC>();
        centro = new GameObject("CenterCir");
        centro.AddComponent<AgentNPC>();
        //metemos los agentes que podemos para la formacion
        foreach (AgentNPC a in agentes) {
            if (asignaciones.Count<ranuras){
                asignaciones.Add(a);
                GameObject ForC = new GameObject("FCE " + asignaciones.Count);
                Agent invisible = ForC.AddComponent<Agent>() as Agent;
                invisible.extRadius=1f;
                invisible.intRadius=1f;
                a.form = true;
            }
        }
        UpdateSlots();
    }
    void Update(){
        foreach (AgentNPC a in asignaciones)
        {
            
        }
    }
    public void UpdateSlots() {

        AgentNPC anchor = GetAnchor();

        anchor.orientation *= -1; 
        
        for (int i = 0; i < asignaciones.Count; i++) {
            Vector3 pos = GetPosition(i);
            float ori = GetOrientation(i);

            var result = new Vector3(Mathf.Cos(anchor.orientation) * pos.x -  Mathf.Sin(anchor.orientation) * pos.z,
                0,
                Mathf.Sin(anchor.orientation) * pos.x + Mathf.Cos(anchor.orientation) * pos.z);
            
            GameObject a = GameObject.Find("FCE " + (i+1));
            Agent invisible = a.GetComponent<Agent>();

            invisible.transform.position =anchor.transform.position + result;
            invisible.orientation =-(anchor.orientation + ori);

            asignaciones[i].GetComponent<SeekAcceleration>().target = invisible;
            asignaciones[i].GetComponent<Align>().target = invisible;
        }
    }

        // calcula la orientacion
    public float GetOrientation(int numero) {

        float anguloOri = numero / (float)asignaciones.Count * Mathf.PI * 2;
        float resultado = anguloOri- Mathf.PI/2;

        return resultado;
    }

    // calcula la posicion
    public Vector3 GetPosition(int numero) {

        float anguloPos = numero / (float)asignaciones.Count * Mathf.PI * 2;
        float radioPos = radio / Mathf.Sin(Mathf.PI / asignaciones.Count);
        Vector3 resultado = new Vector3(radioPos * Mathf.Cos(anguloPos),0,radioPos * Mathf.Sin(anguloPos));
        return resultado;
    }

    public AgentNPC GetAnchor(){
        AgentNPC anchor = centro.GetComponent<AgentNPC>();
        anchor.transform.position = Vector3.zero;
        anchor.orientation =0;

        Vector3 posBase = Vector3.zero;

        for (int i = 0; i < asignaciones.Count; i++) {
            Vector3 pos = GetPosition(i);
            float ori = GetOrientation(i);
            anchor.transform.position += pos;
            anchor.orientation += ori;

            posBase += asignaciones[i].transform.position;
        }

        int num = asignaciones.Count;
        anchor.transform.position /= num;
        anchor.orientation /= num;

        posBase /= num;
        anchor.transform.position += posBase;


        return anchor;
    }
}

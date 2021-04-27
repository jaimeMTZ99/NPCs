﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V : MonoBehaviour
{

    [SerializeField]
    private float radio;

    [SerializeField]
    private float ranuras;
    //lista de los agentes seleccionados
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();
    private GameObject centro;
    private List<AgentNPC> asignaciones;

    void Start() {
        asignaciones = new List<AgentNPC>();
        centro = new GameObject("CenterV");
        centro.AddComponent<AgentNPC>();
        //metemos los agentes que podemos para la formacion
        foreach (AgentNPC a in agentes) {
            if (asignaciones.Count<ranuras){
                asignaciones.Add(a);
                GameObject ForC = new GameObject("V " + asignaciones.Count);
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
            if(a.GetComponent<SeekAcceleration>().target.transform.position == a.transform.position && a.llegar){
                UpdateSlots();
                a.llegar = false;
            }
        }
    }
    public void UpdateSlots() {

        AgentNPC anchor = GetAnchor();

        anchor.orientation *= -1; 
        
        for (int i = 0; i < asignaciones.Count; i++) {
            Vector3 pos = GetPosition(i);
            float ori = 0;

            var result = new Vector3(Mathf.Cos(anchor.orientation) * pos.x -  Mathf.Sin(anchor.orientation) * pos.z,
                0,
                Mathf.Sin(anchor.orientation) * pos.x + Mathf.Cos(anchor.orientation) * pos.z);

            GameObject a = GameObject.Find("V " + (i+1));
            Agent invisible = a.GetComponent<Agent>();

            invisible.transform.position =anchor.transform.position + result;
            invisible.orientation =-(anchor.orientation + ori);

            asignaciones[i].GetComponent<SeekAcceleration>().target = invisible;
            asignaciones[i].GetComponent<Align>().target = invisible;
        }
    }


    // calcula la posicion
    public Vector3 GetPosition(int numero) {

        Vector3 resultado = Vector3.zero;

        if (numero == 0){
            resultado = Vector3.zero;
        }
        else if (numero == 1){
            resultado = new Vector3(radio,0,-radio);
        }
        else if (numero % 2 != 0){
            resultado = new Vector3(radio,0,-radio) + GetPosition(numero-2);
        }
        else if (numero % 2 == 0){
            resultado = new Vector3(-radio,0,-radio) + GetPosition(numero-2);
        }
        return resultado;
    }
    public AgentNPC GetAnchor(){
        AgentNPC anchor = centro.GetComponent<AgentNPC>();
        anchor.transform.position = Vector3.zero;
        anchor.orientation =0;

        Vector3 posBase = Vector3.zero;
        float oriBase = 0f;

        for (int i = 0; i < asignaciones.Count; i++) {
            Vector3 pos = GetPosition(i);
            float ori = 0;
            anchor.transform.position += pos;
            anchor.orientation += ori;

            posBase += asignaciones[i].transform.position;
            oriBase += asignaciones[i].orientation;
        }

        // Divide through to get the drift offset
        int num = asignaciones.Count;
        anchor.transform.position /= num;
        anchor.orientation /= num;

        posBase /= num;
        oriBase /= num;
        anchor.transform.position += posBase;
        anchor.orientation += oriBase;

        return anchor;
    }
}

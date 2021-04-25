using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{

    [SerializeField]
    private float radio;

    private float ranuras = 4;
    //lista de los agentes seleccionados
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();

    private List<AgentNPC> asignaciones;
    private bool vuelta;
    
    private Vector3 pri = new Vector3(0,0,1);
    private Vector3 seg = new Vector3(1,0,0);
    private Vector3 ter = new Vector3(0,0,-1);
    private Vector3 cua = new Vector3(-1,0,0);

    private float ori1 =0;
    private float ori2 =-Mathf.PI/2;
    private float ori3 =Mathf.PI;
    private float ori4 =Mathf.PI/2;

    [SerializeField]
    private Vector3 centro;

    void Start() {
        asignaciones = new List<AgentNPC>();

        //metemos los agentes que podemos para la formacion
        foreach (AgentNPC a in agentes) {
            if (asignaciones.Count<ranuras){
                asignaciones.Add(a);
                GameObject ForC = new GameObject("FC " + asignaciones.Count);
                Agent invisible = ForC.AddComponent<Agent>() as Agent;
                invisible.extRadius=1f;
                invisible.intRadius=1f;
                a.form = true;
            }
        }
        UpdateSlots();
        vuelta = true;
    }
    void Update(){
        foreach (AgentNPC a in asignaciones)
        {
            if (a.llegar){
                centro = a.GetComponent<SeekAcceleration>().target.transform.position;
                GirarMatriz();
                UpdateSlots();
            }
            a.llegar = false;
        }

    }
    public void UpdateSlots() {

        AgentNPC lider = asignaciones[0];

        lider.orientation *= -1; 
        for (int i = 0; i < asignaciones.Count; i++) {
            Vector3 pos = GetPosition(i);
            float ori = GetOrientation(i);

            var result = new Vector3(Mathf.Cos(lider.orientation) * pos.x -  Mathf.Sin(lider.orientation) * pos.z,
                0,
                Mathf.Sin(lider.orientation) * pos.x + Mathf.Cos(lider.orientation) * pos.z);

            GameObject a = GameObject.Find("FC " + (i+1));
            Agent invisible = a.GetComponent<Agent>();


            invisible.transform.position =result;
            invisible.orientation =-(lider.orientation + ori);

            asignaciones[i].GetComponent<SeekAcceleration>().target = invisible;
            asignaciones[i].GetComponent<Align>().target = invisible;
        }
    }
        // calcula la orientacion
    public float GetOrientation(int numero) {

        float resultado;
        if (numero == 0) {
            resultado = 0;
        }
        else if (numero == 1) {
            resultado = -Mathf.PI/2;
        }
        else if (numero == 2) {
            resultado = Mathf.PI;
        }
        else {
            resultado = Mathf.PI/2;
        }
        /**
            if (asignaciones[numero].transform.position == (pri*radio+centro)){
                resultado = ori1;
            } else if(asignaciones[numero].transform.position == (seg*radio+centro)){
                resultado = ori2;
            } else if(asignaciones[numero].transform.position == (ter*radio+centro)){
                resultado = ori3;
            } else {
                resultado = ori4;
            }
                **/
        return resultado;
    }

    public void GirarIzq(){
        Vector3 aux1 = pri;
        Vector3 aux2 = seg;
        Vector3 aux3 = ter;
        Vector3 aux4 = cua;

        pri = aux2;
        seg= aux3;
        ter = aux4;
        cua = aux1;
    }

    public void GirarDer(){
        Vector3 aux1 = pri;
        Vector3 aux2 = seg;
        Vector3 aux3 = ter;
        Vector3 aux4 = cua;
        seg = aux1;
        ter= aux2;
        cua = aux3;
        pri = aux4;
    }

    public void GirarMatriz(){
        if (centro.magnitude - asignaciones[0].transform.position.magnitude >0)
            GirarDer();
        else
            GirarIzq();
        
    }
    // calcula la posicion
    public Vector3 GetPosition(int numero) {

        Vector3 resultado;
        if (numero == 0) {
            resultado = pri * radio + centro;
        }
        else if (numero == 1) {
            resultado = seg * radio + centro;
        }
        else if (numero == 2) {
            resultado = ter * radio + centro;
        }
        else {
            resultado = cua * radio + centro;
        }
        return resultado;
    }

}
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
    
    private Vector3 pri = new Vector3(0,0,1);
    private Vector3 seg = new Vector3(1,0,0);
    private Vector3 ter = new Vector3(0,0,-1);
    private Vector3 cua = new Vector3(-1,0,0);

    private int[] oriGrid;
    private float ori1 =0;
    private float ori2 =-Mathf.PI/2;
    private float ori3 =Mathf.PI;
    private float ori4 =Mathf.PI/2;

    [SerializeField]
    private Vector3 centro;

    void Start() {
        asignaciones = new List<AgentNPC>();
        oriGrid = new int[4];
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
            int oriReal = oriGrid[i];
            float ori = GetOrientation(oriReal);

            GameObject a = GameObject.Find("FC " + (i+1));
            Agent invisible = a.GetComponent<Agent>();


            invisible.transform.position =pos;
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
            if ( new Vector3 (Mathf.Round(asignaciones[numero].transform.position.x/radio - centro.x), 0 ,Mathf.Round(asignaciones[numero].transform.position.z/radio - centro.z)) == pri){
                resultado = ori1;

            } else if(new Vector3 (Mathf.Round(asignaciones[numero].transform.position.x/radio - centro.x), 0 ,Mathf.Round(asignaciones[numero].transform.position.z/radio - centro.z)) == seg){
                resultado = ori2;

            } else if(new Vector3 (Mathf.Round(asignaciones[numero].transform.position.x/radio - centro.x), 0 ,Mathf.Round(asignaciones[numero].transform.position.z/radio - centro.z)) == ter){
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
        Vector3 pri1 = new Vector3(0,0,1);
        Vector3 seg1 = new Vector3(1,0,0);
        Vector3 ter1 = new Vector3(0,0,-1);
        Vector3 cua1 = new Vector3(-1,0,0);



        if (centro.magnitude - asignaciones[0].transform.position.magnitude >0)
            GirarDer();
        else
            GirarIzq();
        


        Debug.Log(pri);
        if(pri == pri1 ){
            for(int i = 0; i< 4; i++)
            {
                oriGrid[i] = i;
            }
        }
        else if (pri == seg1){
            oriGrid[0] = 1;
            oriGrid[1] = 2;
            oriGrid[2] = 3;
            oriGrid[3] = 0;
        }
        else if (pri == ter1){
            oriGrid[0] = 2;
            oriGrid[1] = 3;
            oriGrid[2] = 0;
            oriGrid[3] = 1;
        } else{
            oriGrid[0] = 3;
            oriGrid[1] = 0;
            oriGrid[2] = 1;
            oriGrid[3] = 2;
        }
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
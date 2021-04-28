using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class Fixed : MonoBehaviour
{
    [SerializeField]
    private float radio;
    private float ranuras = 4;
    //lista de los agentes seleccionados
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();
    private List<AgentNPC> asignaciones;
    [SerializeField]
    private int[] oriGrid;
    [SerializeField]
    private Vector3[] posGrid;
    private Vector3[] posGridInicial;

    private GameObject[] invisibles;

    [SerializeField]
    private Vector3 centro;
    private bool llegado;
    private Align align;
    private Face face;
    

    private GameObject f;
    void Start() {
        f = new GameObject("F");
        f.AddComponent<Agent>();
        asignaciones = new List<AgentNPC>();
        oriGrid = new int[4]{0,1,2,3};

        invisibles = new GameObject[4];
        posGridInicial = (Vector3[])posGrid.Clone();
        //metemos los agentes que podemos para la formacion
        int i = 0;
        foreach (AgentNPC a in agentes) {
            if (asignaciones.Count<ranuras){
                asignaciones.Add(a);
                GameObject ForC = new GameObject("FC " + asignaciones.Count);
                Agent invisible = ForC.AddComponent<Agent>() as Agent;
                invisibles[i] = ForC;
                invisible.extRadius=2f;
                invisible.intRadius=2f;
                a.form = true;
                i++;
                face = a.GetComponent<Face>();
                a.SteeringList.Remove(face);
            }
        }
        UpdateSlots();
    }

    void Update(){
        foreach (AgentNPC a in asignaciones)
        {
            if (a.llegar){
                
                centro = a.GetComponent<ArriveAcceleration>().target.transform.position;
                f.GetComponent<Agent>().transform.position = centro;
                for(int i=0;i<4;i++){

                    align = this.transform.GetChild(i).gameObject.GetComponent<Align>();
                    if(asignaciones[i].SteeringList.Contains(align))
                        asignaciones[i].SteeringList.Remove(align);

                    face = asignaciones[i].GetComponent<Face>();
                    asignaciones[i].GetComponent<Face>().aux = f.GetComponent<Agent>();
                    asignaciones[i].GetComponent<Face>().target = f.GetComponent<Agent>();
                    if(!asignaciones[i].SteeringList.Contains(face))
                        asignaciones[i].SteeringList.Add(face);
                }
                Move();

                if((Mathf.Abs(a.transform.position.x) - Mathf.Abs(f.transform.position.x) < a.intRadius) && (Mathf.Abs(a.transform.position.z) - Mathf.Abs(f.transform.position.z) < a.intRadius))
                {
                    llegado =true;
                    a.llegar = false;
                }

            } else if(asignaciones[0].Velocity.magnitude == 0 && llegado == true){
                for(int i=0;i<4;i++){
                    align = asignaciones[i].GetComponent<Align>();
                    if(!asignaciones[i].SteeringList.Contains(align))
                        asignaciones[i].SteeringList.Add(align);

                    face = asignaciones[i].GetComponent<Face>();
                    if(asignaciones[i].SteeringList.Contains(face))
                        asignaciones[i].SteeringList.Remove(face);
                }
                GirarMatriz();
                UpdateSlots();
                llegado = false;
            }


        }

    }

    public void Move(){
        for (int i = 0; i < asignaciones.Count; i++) {
            GameObject a = invisibles[i];
            Agent invisible = a.GetComponent<Agent>();
            invisible.transform.position =centro;
            asignaciones[i].GetComponent<ArriveAcceleration>().target = invisible;
        }  
    }

    public void UpdateSlots() {
        
        for (int i = 0; i < asignaciones.Count; i++) {
            Vector3 pos = GetPosition(i);
            int oriReal = oriGrid[i];
            float ori = GetOrientation(oriReal);

            GameObject a = invisibles[i];
            Agent invisible = a.GetComponent<Agent>();

            invisible.transform.position =pos;
            invisible.orientation =ori;
            asignaciones[i].GetComponent<ArriveAcceleration>().target = invisible;
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
            resultado = Mathf.PI/2;
        }
        else if (numero == 2) {
            resultado = Mathf.PI;
        }
        else {
            resultado = -Mathf.PI/2;
        }  
        return resultado;
    }
    public void GirarIzq(){
        Vector3 aux1 = posGrid[0];
        Vector3 aux2 = posGrid[1];
        Vector3 aux3 = posGrid[2];
        Vector3 aux4 =posGrid[3];
        posGrid[0] = aux2;
        posGrid[1]= aux3;
        posGrid[2] = aux4;
        posGrid[3] = aux1;
    }
    public void GirarDer(){
        Vector3 aux1 = posGrid[0];
        Vector3 aux2 = posGrid[1];
        Vector3 aux3 = posGrid[2];
        Vector3 aux4 =posGrid[3];
        posGrid[0] = aux4;
        posGrid[1]= aux1;
        posGrid[2] = aux2;
        posGrid[3] = aux3;
    }
    public void GirarMatriz(){

        if (centro.z - asignaciones[0].transform.position.z > radio*4 ){

            while(posGrid[0] != posGridInicial[0]){
                GirarDer();
            }
        }else if (centro.z - asignaciones[0].transform.position.z < -radio*4){

            while(posGrid[0] != posGridInicial[2]){
                GirarIzq();
            }
        }
        else if (centro.x - asignaciones[0].transform.position.x >0){

            while (posGrid[0] != posGridInicial[1]){
                GirarDer();
            }
        }
        else{

            while (posGrid[0] != posGridInicial[3]){
                GirarIzq();
            }
        }
        
        if(posGrid[0] == posGridInicial[0] ){
            for(int i = 0; i< 4; i++)
            {
                oriGrid[i] = i;
            }
        }
        else if (posGrid[0] == posGridInicial[1]){
            oriGrid[0] = 1;
            oriGrid[1] = 2;
            oriGrid[2] = 3;
            oriGrid[3] = 0;
        }
        else if (posGrid[0] == posGridInicial[2]){
            oriGrid[0] = 2;
            oriGrid[1] = 3;
            oriGrid[2] = 0;
            oriGrid[3] = 1;
        } else if (posGrid[0] == posGridInicial[3]){
            oriGrid[0] = 3;
            oriGrid[1] = 0;
            oriGrid[2] = 1;
            oriGrid[3] = 2;
        }
    }
    // calcula la posicion
    public Vector3 GetPosition(int numero) {
        Vector3 resultado;
        resultado = posGrid[numero] * radio + centro;
        return resultado;
    }
}
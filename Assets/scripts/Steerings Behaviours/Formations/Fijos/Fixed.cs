using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed : MonoBehaviour
{
    private const int V = 0;
    [SerializeField]
    private float radio;

    private float ranuras = 4;
    //lista de los agentes seleccionados
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();

    private List<AgentNPC> asignaciones;
    
    private int[] oriGrid;
    [SerializeField]
    private Vector3[] posGrid;
    private Vector3[] posGridInicial;
     [SerializeField]
    private float[] oriGridInicial;
    private GameObject[] invisibles;

    [SerializeField]
    private Vector3 centro;



    void Start() {
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
                asignaciones[i].GetComponent<Face>().aux = invisible;
                a.form = true;
                i++;
                
            }
        }
        UpdateSlots();

    }
    void Update(){
        int i = 0;
        Agent invisible;
        SteeringBehaviour face;
        foreach (AgentNPC a in asignaciones)
        {
            invisible = invisibles[i].GetComponent<Agent>();
            if (a.llegar){
                face = a.GetComponent<Face>();
                a.addSteering(face);
                Face fCast = (Face) face;
                fCast.aux =  invisibles[i].GetComponent<Agent>();
                centro = a.GetComponent<SeekAcceleration>().target.transform.position;
                GirarMatriz();
                UpdateSlots();
            }else if (a.velocity.magnitude == 0){
                int oriReal = oriGrid[i];
                invisible.orientation = GetOrientation(oriReal); 
                face = a.GetComponent<Face>(); 
                a.removeSteering(face);
                a.GetComponent<Align>().target = invisible;
            }
            a.llegar = false;
            i++;
        }

    }
    public void UpdateSlots() {

        AgentNPC lider = asignaciones[0];

         
        for (int i = 0; i < asignaciones.Count; i++) {
            Vector3 pos = GetPosition(i);
           // int oriReal = oriGrid[i];
           // float ori = GetOrientation(oriReal);

            GameObject a = invisibles[i];
            Agent invisible = a.GetComponent<Agent>();


            invisible.transform.position =pos;
            invisible.orientation= 0;
            //invisible.orientation =ori;

            asignaciones[i].GetComponent<SeekAcceleration>().target = invisible;
            asignaciones[i].GetComponent<Face>().target = invisible;
            //asignaciones[i].GetComponent<Align>().target = invisible;
        }
    }
        // calcula la orientacion
    public float GetOrientation(int numero) {

        float resultado;
        if (numero == 0) {
            resultado = oriGridInicial[0];
        }
        else if (numero == 1) {
            resultado = oriGridInicial[1];
        }
        else if (numero == 2) {
            resultado = oriGridInicial[2];
        }
        else {
            resultado = oriGridInicial[3];
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
       /* Vector3 pri1 = new Vector3(0,0,1);
        Vector3 seg1 = new Vector3(1,0,0);
        Vector3 ter1 = new Vector3(0,0,-1);
        Vector3 cua1 = new Vector3(-1,0,0);*/
        if (centro.z - asignaciones[0].transform.position.z > radio*4 ){
            Debug.Log("Arriba");
            while(posGrid[0] != posGridInicial[0]){
                GirarDer();
            }
        }else if (centro.z - asignaciones[0].transform.position.z < -radio*4){
             Debug.Log("Abajo");
            while(posGrid[0] != posGridInicial[2]){
                GirarIzq();
            }
        }
        else if (centro.x - asignaciones[0].transform.position.x >0){
            Debug.Log("Derecha");
            while (posGrid[0] != posGridInicial[1]){
                GirarDer();
            }
        }
        else{
            Debug.Log("Izquierda");
            while (posGrid[0] != posGridInicial[3]){
                GirarIzq();
            }
        }
        
        Debug.Log(posGrid[0]);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEsc : FormationManager
{

    /**[SerializeField]
    private float radio;

    [SerializeField]
    private float ranuras;
    //lista de los agentes seleccionados
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();
    private List<AgentNPC> asignaciones;
    private GameObject centro;
    **/
    private bool llegado;
    private GameObject f;
    private GameObject p;
    private Vector3 centro1;
    private Pursue pursue;
    private bool lider = true;
    void Start() {
        f = new GameObject("F1");
        f.AddComponent<Agent>();
        asignaciones = new List<AgentNPC>();
        p = new GameObject("lugar");
        p.AddComponent<Agent>();
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
                face = a.GetComponent<Face>();
                a.SteeringList.Remove(face);

                if(lider == false)
                {
                    pursue = a.GetComponent<Pursue>();
                    a.SteeringList.Remove(pursue);
                }
                lider =false;
            }
        }
        UpdateSlots();
    }




    void Update(){
        foreach (AgentNPC a in asignaciones)
        {
            if (a.llegar){
                
                centro1 = a.GetComponent<ArriveAcceleration>().target.transform.position;
                f.GetComponent<Agent>().transform.position = centro1;
                centro.transform.position = centro1;
                for(int i=0;i<asignaciones.Count;i++){

                    align = this.transform.GetChild(i).gameObject.GetComponent<Align>();
                    if(asignaciones[i].SteeringList.Contains(align))
                        asignaciones[i].SteeringList.Remove(align);

                    face = asignaciones[i].GetComponent<Face>();
                    asignaciones[i].GetComponent<Face>().aux = f.GetComponent<Agent>();
                    asignaciones[i].GetComponent<Face>().target = f.GetComponent<Agent>();
                    if(!asignaciones[i].SteeringList.Contains(face))
                        asignaciones[i].SteeringList.Add(face);
                    if(i != 0){
                        pursue = asignaciones[i].GetComponent<Pursue>();
                        pursue.target = asignaciones[0];
                        pursue.aux = asignaciones[0];
                        if(!asignaciones[i].SteeringList.Contains(pursue))
                            asignaciones[i].SteeringList.Add(pursue);
                    }
                }
                Move();

                if((Mathf.Abs(asignaciones[0].transform.position.x) - Mathf.Abs(f.transform.position.x) < asignaciones[0].intRadius) && (Mathf.Abs(asignaciones[0].transform.position.z) - Mathf.Abs(f.transform.position.z) < asignaciones[0].intRadius))
                {
                    llegado =true;
                    a.llegar = false;
                }

            } else if(asignaciones[0].Velocity.magnitude == 0 && llegado == true){
                for(int i=0;i<asignaciones.Count;i++){
                    align = asignaciones[i].GetComponent<Align>();
                    if(!asignaciones[i].SteeringList.Contains(align))
                        asignaciones[i].SteeringList.Add(align);

                    face = asignaciones[i].GetComponent<Face>();
                    if(asignaciones[i].SteeringList.Contains(face))
                        asignaciones[i].SteeringList.Remove(face);
                    if(i != 0){
                        pursue = asignaciones[i].GetComponent<Pursue>();
                        if(asignaciones[i].SteeringList.Contains(pursue))
                            asignaciones[i].SteeringList.Remove(pursue);
                    }
                }
                UpdateSlots();
                llegado = false;
            }

        }

    }


    
    public void Move(){
        Agent invisible = p.GetComponent<Agent>();
        invisible.transform.position =centro1;
        asignaciones[0].GetComponent<ArriveAcceleration>().target = invisible;

    }


    public override void UpdateSlots() {

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

            asignaciones[i].GetComponent<ArriveAcceleration>().target = invisible;
            asignaciones[i].GetComponent<Align>().target = invisible;
        }
    }

        // calcula la orientacion
    public override float GetOrientation(int numero) {

        float anguloOri = numero / (float)asignaciones.Count * Mathf.PI * 2;
        float resultado = anguloOri- Mathf.PI/2;

        return resultado;
    }

    // calcula la posicion
    public override Vector3 GetPosition(int numero) {

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

    public override bool SupportsSlots(int slotCount) {   
        return true;
    }
}

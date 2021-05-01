using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccion : MonoBehaviour
{
    public float tiempoVuelta =10;
    private float timeRes;
    public List<GameObject> selectedUnits;
    public GameObject selectedUnit;
    private GameObject goSel;
    private bool mult = false;
    private Agent t;
    private Agent t1;

    private List<GameObject> agentesArrive;
    private List<Agent> targetsArrive;


    void Start(){
        timeRes=tiempoVuelta;
        agentesArrive = new List<GameObject>();
        targetsArrive = new List<Agent>();
    }
    // Update is called once per frame
    void Update()
    {

        timeRes -= Time.deltaTime;
        if (timeRes <=0.0f){
            timeRes = tiempoVuelta;
            Debug.Log("timer");

            //TimeUp();
        }
        Selec();       
    }

    private void Selec(){

        //Si pulsamos Control, sera para coger varios individuos para moverse
        mult = Input.GetKey(KeyCode.LeftControl);
        // si clickamos el boton izq del raton, es para seleccionar individiuos.
        if (Input.GetMouseButtonUp(0))
        {
            seleccionarPersonajes();
        }
        if (Input.GetMouseButtonUp(1)){
            DirigirLugar();
        }
        
    }


    public void seleccionarPersonajes(){
        
            // Comprobamos si el ratón golpea a algo en el escenario.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {

                // Si lo que golpea es un punto del terreno entonces da la orden a todas las unidades NPC
                if (hitInfo.collider != null && hitInfo.collider.CompareTag("NPC") && mult)
                {

                    selectedUnits.Add(hitInfo.collider.gameObject);
                    foreach (GameObject u in selectedUnits)
                    {
                        GameObject p = u.transform.Find("Sel").gameObject;
                        p.SetActive(true);
                    }
                    if(selectedUnit != null){
                        GameObject s = selectedUnit.transform.Find("Sel").gameObject;
                        s.SetActive(false);                        
                    }
                    selectedUnit=null; 
                }
                else if(hitInfo.collider != null && hitInfo.collider.CompareTag("NPC")){
                    if (selectedUnit != null){
                        GameObject g = selectedUnit.transform.Find("Sel").gameObject;
                        g.SetActive(false);
                        selectedUnit=null;
                    }
                    selectedUnit = hitInfo.collider.gameObject;
                    GameObject s = selectedUnit.transform.Find("Sel").gameObject;
                    s.SetActive(true);

                    if(selectedUnits.Count>0){
                        foreach (GameObject u in selectedUnits)
                        {
                            GameObject p = u.transform.Find("Sel").gameObject;
                            p.SetActive(false);
                        }
                    }
                    selectedUnits.Clear();  
                    
                }
                else if (hitInfo.collider != null && !hitInfo.collider.CompareTag("NPC")){
                    if(selectedUnit != null){
                        GameObject s = selectedUnit.transform.Find("Sel").gameObject;
                        s.SetActive(false);                        
                    }
                    if(selectedUnits.Count>0){
                        foreach (GameObject u in selectedUnits)
                        {
                            GameObject p = u.transform.Find("Sel").gameObject;
                            p.SetActive(false);
                        }
                    }                    
                    selectedUnits.Clear();
                    selectedUnit=null;
                    
                }
        
            }
    }

    public void DirigirLugar(){
        

            // Comprobamos si el ratón golpea a algo en el escenario.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float timeRet = 0;
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {

                // Si lo que golpea es un punto del terreno entonces da la orden a todas las unidades NPC
                if (hitInfo.collider != null && hitInfo.collider.CompareTag("Terrain") && (selectedUnit != null || selectedUnits.Count > 0))
                {


                    if(selectedUnits.Count>0){
                        goSel = new GameObject("Seleccion Grupo");
                        Agent invisible = goSel.AddComponent<Agent>();
                        t = invisible;
                        Vector3 newTarget = hitInfo.point;
                        t.transform.position = newTarget;
                        t.transform.position += new Vector3 (0,1.3f,0);
                        t.extRadius=2;
                        t.intRadius=2;
                        foreach (GameObject npc in selectedUnits)
                        {

                            ArriveAcceleration d = npc.GetComponent<ArriveAcceleration>();
                            AgentNPC x = npc.GetComponent<AgentNPC>();
                            if (x.form){
                                x.llegar = true;
                                d = npc.GetComponent<ArriveAcceleration>();
                                d.target = t;
                                Align c = npc.GetComponent<Align>();
                                c.target = t;
                            }
                            if(d == null){
                                x.nuevoArrive=true;
                                npc.AddComponent<ArriveAcceleration>();
                                d = npc.GetComponent<ArriveAcceleration>();
                                d.target = t;
                            }
                            else
                            {
                                if(x.nuevoArrive == false){
                                    targetsArrive.Add(d.target);
                                    agentesArrive.Add(npc);
                                }
                                d.target = t;
                            }

                            AgentNPC n = npc.GetComponent<AgentNPC>();

                            if(!n.SteeringList.Contains(d))
                            {
                                n.SteeringList.Add(d);
                            }
                        }                
                    } else{
                            goSel = new GameObject("Seleccion Solo");
                            Agent invisible = goSel.AddComponent<Agent>();
                            t1 = invisible;
                            Vector3 newTarget1 = hitInfo.point;
                            t1.transform.position = newTarget1;
                            t1.transform.position += new Vector3 (0,1.3f,0);
                            t1.extRadius=2;
                            t1.intRadius=2;

                            ArriveAcceleration e = selectedUnit.GetComponent<ArriveAcceleration>();
                            AgentNPC x = selectedUnit.GetComponent<AgentNPC>();
                            if (x.form){
                                x.llegar = true;
                                e = selectedUnit.GetComponent<ArriveAcceleration>();
                                e.target = t1;
                                Align c = selectedUnit.GetComponent<Align>();
                                c.target = t1;
                            }

                            if(e == null){
                                selectedUnit.AddComponent<ArriveAcceleration>();
                                e = selectedUnit.GetComponent<ArriveAcceleration>();
                                x.nuevoArrive=true;
                                e.target = t1;

                            }
                            else
                            {
                                if(x.nuevoArrive == false){
                                    targetsArrive.Add(e.target);
                                    agentesArrive.Add(selectedUnit);
                                }
                                e.target = t1;

                            }
                            AgentNPC n = selectedUnit.GetComponent<AgentNPC>();
                            if(!n.SteeringList.Contains(e))
                            {
                                n.SteeringList.Add(e);
                            }                          
                    }
                }
        
            }
    }
    /**public void TimeUp(){
        
        if(agentesArrive.Contains(t.g))
        {
            t.g.GetComponent<ArriveAcceleration>().target =null;// targetsArrive[0];
            targetsArrive.Clear();
            agentesArrive.Clear();
            
        }
        else
        {
            Destroy(t.g.GetComponent<ArriveAcceleration>());
            AgentNPC n = t.g.GetComponent<AgentNPC>(); 
            n.SteeringList.Remove(selectedUnit.GetComponent<ArriveAcceleration>());
        }
        
    }**/
}
    


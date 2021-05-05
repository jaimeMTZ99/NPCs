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

    private List<GameObject> agentesRetForms;
    private List<GameObject> agentesReturn; //lista con los gameObject que vuelven a la normalidad

    public List<GameObject> listPuntos = new List<GameObject>();    //variables para pathfinding
    public PathFinding pathFinding;
    public List<GameObject> camino;

    void Start(){
        timeRes=tiempoVuelta;           //establecemos el tiempo inicial y las listas vacias
        agentesReturn = new List<GameObject>();
        agentesRetForms = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {

        timeRes -= Time.deltaTime;      //vamos quitando tiempo hasta que lleguemos a 0 y el tiempo se resetee y llamemos a la funcion que devuelve todo a la normalidad
        if (timeRes <=0.0f){
            timeRes = tiempoVuelta;

            TimeUp();
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
                else if(hitInfo.collider != null && (hitInfo.collider.CompareTag("NPC") || hitInfo.collider.CompareTag("PathFinding"))){
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
        
            if (selectedUnit != null && selectedUnit.tag == "PathFinding"){
                Pathfinding();
            } else{

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
                        Debug.Log("Time to return");
                        timeRes = tiempoVuelta;                     //reseteamos tiempo
                        foreach (GameObject npc in selectedUnits)
                        {
                            AgentNPC n = npc.GetComponent<AgentNPC>();
                            ArriveAcceleration d = npc.GetComponent<ArriveAcceleration>();
                            BlendedSteering l = npc.GetComponent<BlendedSteering>();
                            AgentNPC x = npc.GetComponent<AgentNPC>();
                            if (x.form && npc.name == "lider"){
                                x.llegar = true;
                                d = npc.GetComponent<ArriveAcceleration>();
                                d.target = t;
                                Align c = npc.GetComponent<Align>();
                                c.target = t;
                            } else if (x.form && npc.name != "lider"){
                                x.form = false;
                                npc.transform.parent.gameObject.GetComponent<CircleFixed>().agentes.Remove(n);
                                agentesRetForms.Add(npc);
                            }
                            if(d == null){

                                if(l == null)

                                {
                                foreach (SteeringBehaviour i in npc.GetComponents<SteeringBehaviour>())
                                {
                                    n.SteeringList.Remove(i);
                                }
                                }else {
                                    l.behaviours.Clear();
                                }

                                x.nuevoArrive=true;
                                npc.AddComponent<ArriveAcceleration>();
                                d = npc.GetComponent<ArriveAcceleration>();
                                d.target = t;
                                agentesReturn.Add(npc);

                                if(l != null){
                                    l.behaviours.Add(d);
                                }
                            }
                            else
                            {
                                d.target = t;
                            }


                            if(!n.SteeringList.Contains(d) && l == null)
                            {
                                n.SteeringList.Add(d);
                            }
                        }                
                    } else{
                            goSel = new GameObject("Seleccion Solo");               //crear el agente invisible que sera target del nuevo arrive
                            Agent invisible = goSel.AddComponent<Agent>();
                            t1 = invisible;
                            Vector3 newTarget1 = hitInfo.point;
                            t1.transform.position = newTarget1;
                            t1.transform.position += new Vector3 (0,1.3f,0);
                            t1.extRadius=2;
                            t1.intRadius=2;

                            timeRes = tiempoVuelta;                     //reseteamos tiempo

                            ArriveAcceleration e = selectedUnit.GetComponent<ArriveAcceleration>();     //sacamos el arrive y el agenteNPC
                            AgentNPC x = selectedUnit.GetComponent<AgentNPC>();
                            if (x.form && selectedUnit.name == "lider"){                                //si es el lider de la formacion dirigir la formacion entera
                                x.llegar = true;
                                e = selectedUnit.GetComponent<ArriveAcceleration>();
                                e.target = t1;
                                Align c = selectedUnit.GetComponent<Align>();
                                c.target = t1;
                            } else if (x.form && selectedUnit.name != "lider"){
                                x.form = false;
                                selectedUnit.transform.parent.gameObject.GetComponent<CircleFixed>().agentes.Remove(x);
                                agentesRetForms.Add(selectedUnit);
                            }                                                       //si pertenece a la formacion pero no es el lider, que se salga de la formacion

                            BlendedSteering m = selectedUnit.GetComponent<BlendedSteering>();
                            AgentNPC n = selectedUnit.GetComponent<AgentNPC>();         //se añade a la lista de steering behaviours paraque tengan ese comportamiento
                            if(e == null){                                                              //si no tiene arrive, se le anade con el nuevo target y se le pone nuevoArrive a true para restablecerlo despues
                                

                                if(m == null)

                                {
                                    foreach (SteeringBehaviour i in selectedUnit.GetComponents<SteeringBehaviour>())
                                    {
                                        n.SteeringList.Remove(i);
                                    }
                                }else {
                                    m.behaviours.Clear();
                                }

                                
                                selectedUnit.AddComponent<ArriveAcceleration>();
                                e = selectedUnit.GetComponent<ArriveAcceleration>();
                                x.nuevoArrive=true;
                                e.target = t1;

                                if(m != null){
                                    m.behaviours.Add(e);
                                }

                                agentesReturn.Add(selectedUnit);

                            }
                            else                                     //si ya lo tenia, y nuevoArrive esta a true solo se le asigna el nuevo target, si no se le anade a la lista del target y el gameobject
                            {
                                e.target = t1;
                            }
                            if(!n.SteeringList.Contains(e) && m == null)
                            {
                                n.SteeringList.Add(e);
                            }  
                    }
                }
        
            }
    }

    }


    public void TimeUp(){


        foreach (GameObject h in agentesRetForms)
        {
            AgentNPC v = h.GetComponent<AgentNPC>();
            if(v.form ==false){
                h.transform.parent.gameObject.GetComponent<CircleFixed>().agentes.Add(v);
                v.form =true;
            }
        }
        agentesRetForms.Clear();
        foreach (GameObject g in agentesReturn)
        {
            BlendedSteering m = g.GetComponent<BlendedSteering>();
            AgentNPC n = g.GetComponent<AgentNPC>();
            ArriveAcceleration e = g.GetComponent<ArriveAcceleration>();
            Debug.Log(g.transform.name);
            if (m == null){
                if (n.nuevoArrive)
                {
                    n.SteeringList.Remove(e);
                    Destroy(e);
                    n.nuevoArrive = false;
                    foreach (SteeringBehaviour i in g.GetComponents<SteeringBehaviour>())
                    {
                        if(i != e)
                            n.SteeringList.Add(i);
                    }

                }
            } else{
                if (n.nuevoArrive)
                {
                    m.behaviours.Remove(e);
                    Destroy(e);
                    n.nuevoArrive = false;
                    foreach (SteeringBehaviour i in g.GetComponents<SteeringBehaviour>())
                    {
                        if(i != e && i != m)
                            m.behaviours.Add(i);
                    }
                }   
            }
        }
        agentesReturn.Clear();
        
    }

    void Pathfinding(){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            PathFollowing pf = new PathFollowing() ;
            Path path  = new Path();
            pathFinding = selectedUnit.GetComponent<PathFinding>();
            
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform != null && hit.transform.tag != "Muro" && hit.transform.tag != "Agua")
                {
                    AgentNPC n = selectedUnit.GetComponent<AgentNPC>();
                    listPuntos = pathFinding.EstablecerNodoFinal(n);
                    if (selectedUnit.GetComponent<PathFollowing>() == null){
                        pf =  selectedUnit.AddComponent(typeof(PathFollowing)) as PathFollowing;
                        path = selectedUnit.AddComponent(typeof(Path)) as Path;
                        pf.path = selectedUnit.GetComponent<Path>();
                        pf.path.Radio = 2f;
                        n.SteeringList.Add(pf);
                    }
                    path = selectedUnit.GetComponent<Path>();
                    path.ClearPath();
                    pf = selectedUnit.GetComponent<PathFollowing>();
                    pf.currentPos = 0;
                    for(int i =0 ; i< listPuntos.Count; i ++){
                        path.AppendPointToPath(listPuntos[i]);
                    }
                    pintarCamino();
                }
            }
    }

    void pintarCamino()
    {
        if (listPuntos.Count != 0)
        {
            if (camino != null)
            {
                foreach (GameObject g in camino)
                {
                      Destroy(g);
                }
            }
            
            camino = new List<GameObject>();
            GameObject aux;
            foreach(GameObject v in listPuntos)
            {
                aux = GameObject.CreatePrimitive(PrimitiveType.Cube);
                aux.transform.localScale = new Vector3(1, 2, 1);
                aux.transform.position = v.transform.position;
                //aux.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                camino.Add(aux);
            }
            
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNPC : Agent
{
    //Esto es para el pathfinding
    public List<GameObject> listPuntos = new List<GameObject>();
    public List<SteeringBehaviour> SteeringList;
    [SerializeField]
    private Steering steer;
    [SerializeField]
    public float blendWeight;
    public PathFinding pathFinding;

    public List<GameObject> camino;
    void Awake()
    {
        if(this.gameObject.GetComponent<PrioritySteering>()!= null)
        {
            SteeringList.Add(this.gameObject.GetComponent<PrioritySteering>());
        }
        else if(this.gameObject.GetComponent<PrioritySteering1>() != null)
        {
            SteeringList.Add(this.gameObject.GetComponent<PrioritySteering1>());
        }
        else if (this.gameObject.GetComponent<BlendedSteering>() != null){
            SteeringList.Add(this.gameObject.GetComponent<BlendedSteering>());
        }
        else{
            foreach (SteeringBehaviour s in this.gameObject.GetComponents<SteeringBehaviour>())
            {
                SteeringList.Add(s);
            }
        }
        //SteeringList = this.gameObject.GetComponents<SteeringBehaviour>();
    }
    void Update(){
        if (Input.GetMouseButtonDown(0) && this.gameObject.tag == "PathFinding")
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            PathFollowing pf = new PathFollowing() ;
            Path path  = new Path();
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform != null && hit.transform.tag != "Muro" && hit.transform.tag != "Agua")
                {
                    listPuntos = pathFinding.EstablecerNodoFinal(this);
                    if (this.gameObject.GetComponent<PathFollowing>() == null){
                        pf =  this.gameObject.AddComponent(typeof(PathFollowing)) as PathFollowing;
                        path = this.gameObject.AddComponent(typeof(Path)) as Path;
                        pf.path = this.gameObject.GetComponent<Path>();
                        pf.path.Radio = 1f;
                        SteeringList.Add(pf);
                    }
                    Debug.Log("movimiento");
                    path = this.gameObject.GetComponent<Path>();
                    path.ClearPath();
                    pf = this.gameObject.GetComponent<PathFollowing>();
                   // pf.currentParam = 0;
                    pf.currentPos = 0;
                    for(int i =0 ; i< listPuntos.Count; i ++){
                        //Debug.Log(listPuntos[i].transform.position);
                        path.AppendPointToPath(listPuntos[i]);
                    }
                    pintarCamino();
                }
            }
        }
    }
    void FixedUpdate()
    {
        PrioritySteering ps = this.gameObject.GetComponent<PrioritySteering>();
        PrioritySteering1 ps1 = this.gameObject.GetComponent<PrioritySteering1>();
        BlendedSteering arbitro = this.gameObject.GetComponent<BlendedSteering>();

        if(ps != null){
            steer = ps.GetSteering(this);
            applySteering(steer);
        }
        else if(ps1 != null){
            steer = ps1.GetSteering(this);
            applySteering(steer);            
        } else if (arbitro == null) {
            if (SteeringList != null)
            {
                foreach (SteeringBehaviour s in SteeringList)
                {
                    steer = s.GetSteering(this);
                    applySteering(steer);
                }
            }
        }
        else {
            steer = arbitro.GetSteering(this);
            applySteering(steer);
        }
    }
    public void applySteering(Steering s)
    {
        Vector3 Acceleration = s.linear / mass;       // A = F/masa
        Rotation = s.angular;
        Position += Velocity * Time.deltaTime; // Fórmulas de Newton
        Orientation += Rotation * Time.deltaTime; //Radianes
        Velocity += Acceleration * Time.deltaTime;  // Aceleracion usando el tiempo      

        transform.rotation = new Quaternion(); //Quaternion.identity;
        transform.Rotate(Vector3.up, Orientation * Mathf.Rad2Deg);
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
                aux.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                camino.Add(aux);
            }
            
        }
    }
}

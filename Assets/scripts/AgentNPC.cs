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
            PathFollowing pf = new PathFollowing();
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform != null && hit.transform.tag != "Muro" && hit.transform.tag != "Agua")
                {
                    listPuntos = pathFinding.EstablecerNodoFinal(this);
                    Debug.Log("Antes" + listPuntos.Count);
                    if (this.gameObject.GetComponent<PathFollowing>() == null){
                        pf =  this.gameObject.AddComponent(typeof(PathFollowing)) as PathFollowing;
                        SteeringList.Add(pf);
                    }
                    pf.path.ClearPath();
                    for(int i =0 ; i< listPuntos.Count; i ++){
                        Debug.Log(listPuntos[i].transform.position);
                        pf.path.AppendPointToPath(listPuntos[i]);
                    }
                    Debug.Log(pf.path + "despues");
                    //pintarCamino();
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
                Debug.Log("Que pasaria si la lista se queda vacia");
            }
            else
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

}

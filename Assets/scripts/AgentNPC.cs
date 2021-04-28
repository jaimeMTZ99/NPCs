using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNPC : Agent
{

    public List<SteeringBehaviour> SteeringList;
    [SerializeField]
    private Steering steer;
    [SerializeField]
    public float blendWeight;

    void Awake()
    {
        foreach (SteeringBehaviour s in this.gameObject.GetComponents<SteeringBehaviour>())
        {
            SteeringList.Add(s);
        }
        //SteeringList = this.gameObject.GetComponents<SteeringBehaviour>();
    }
    void FixedUpdate()
    {
        BlendedSteering arbitro = this.gameObject.GetComponent<BlendedSteering>();
        if (arbitro == null) {

            foreach (SteeringBehaviour s in SteeringList)
            {
                steer = s.GetSteering(this);
                applySteering(steer);
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
    /**public void removeSteering(SteeringBehaviour steer){
        bool encontrado = false;
        for (int k = 0 ;  k < this.SteeringList.Length; k++){
            if (SteeringList[k] == steer){
                encontrado = true;
            }
        }
        if (encontrado){
            SteeringBehaviour[] SteeringListAux = new SteeringBehaviour[SteeringList.Length-1];
            int j = 0;
            for (int i = 0; i < this.SteeringList.Length;i++){
                if (SteeringList[i] != steer){
                    Debug.Log("i: " + i + " j: " + j);
                    SteeringListAux[j] = SteeringList[i];
                    j++;
                }
            }
            this.SteeringList = SteeringListAux;
        }
    }**/
    /**public void addSteering(SteeringBehaviour steer){
        int nuevoTamaño = SteeringList.Length+1;
        SteeringBehaviour[] SteeringListAux = new SteeringBehaviour[nuevoTamaño];
        for (int i = 0; i < this.SteeringList.Length;i++){
            SteeringListAux[i] = SteeringList[i];
        }
        SteeringListAux[nuevoTamaño-1] = steer;
        this.SteeringList = SteeringListAux;
    }**/
}

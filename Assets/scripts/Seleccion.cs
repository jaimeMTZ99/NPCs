using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccion : MonoBehaviour
{

    public List<GameObject> selectedUnits;
    public GameObject selectedUnit;
    private GameObject goSel;
    private bool mult = false;
    private Agent t;
    private Agent t1;

    // Update is called once per frame
    void Update()
    {
        Selec();
    }

    private void Selec(){

                //Si pulsamos Control, sera para coger varios individuos para moverse
        mult = Input.GetKey(KeyCode.LeftControl);
        // si clickamos el boton izq del raton, es para seleccionar individiuos.
        if (Input.GetMouseButtonUp(0))
        {

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
                    selectedUnit=null;  //poner a false el outline
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
        if (Input.GetMouseButtonUp(1)){


            // Comprobamos si el ratón golpea a algo en el escenario.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
                            SeekAcceleration d = npc.GetComponent<SeekAcceleration>();
                            AgentNPC x = npc.GetComponent<AgentNPC>();
                            if (x.form){
                                d = npc.GetComponent<SeekAcceleration>();
                                d.target = t;
                                Align c = npc.GetComponent<Align>();
                                c.target = t;
                            }
                            if(d == null){
                                npc.AddComponent<SeekAcceleration>();
                                d = npc.GetComponent<SeekAcceleration>();
                                d.target = t;
                            }
                            else
                            {
                                d.target = t;
                            }

                            AgentNPC n = npc.GetComponent<AgentNPC>();
                            bool esta = false;
                            foreach (SteeringBehaviour i in n.SteeringList)
                            {
                                if (i == d)
                                {
                                    esta = true;
                                }
                            } 
                            if(!esta){
                                n.SteeringList= npc.GetComponents<SteeringBehaviour>();
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

                            SeekAcceleration e = selectedUnit.GetComponent<SeekAcceleration>();
                            AgentNPC x = selectedUnit.GetComponent<AgentNPC>();
                            if (x.form){
                                e = selectedUnit.GetComponent<SeekAcceleration>();
                                e.target = t1;
                                Align c = selectedUnit.GetComponent<Align>();
                                c.target = t1;
                            }

                            if(e == null){
                                selectedUnit.AddComponent<SeekAcceleration>();
                                e = selectedUnit.GetComponent<SeekAcceleration>();

                                e.target = t1;

                            }
                            else
                            {
                                e.target = t1;

                            }
                            AgentNPC n = selectedUnit.GetComponent<AgentNPC>();
                            bool esta = false;
                            foreach (SteeringBehaviour i in n.SteeringList)
                            {
                                if (i == e)
                                {
                                    esta = true;
                                }
                            } 
                            if(!esta){
                                n.SteeringList= selectedUnit.GetComponents<SteeringBehaviour>();
                            }
                    }
                }
        
            }
        }

    }
}
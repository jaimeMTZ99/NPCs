using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FormationManager : MonoBehaviour {
    /*
    [SerializeField]
    private float radio;


    [SerializeField]
    private float ranuras;
    //lista de los agentes seleccionados
    [SerializeField]
    private List<AgentNPC> agentes = new List<AgentNPC>();
    private GameObject centro;
    private List<AgentNPC> asignaciones;
    private GameObject[] invisibles;

    void Start() {
        asignaciones = new List<AgentNPC>();
        centro = new GameObject("Center");
        invisibles = new GameObject[4];
        centro.AddComponent<AgentNPC>();
        //metemos los agentes que podemos para la formacion
        foreach (AgentNPC a in agentes) {
            if (asignaciones.Count<ranuras){
                asignaciones.Add(a);
                GameObject ForC = new GameObject("V " + asignaciones.Count);
                invisibles[i] = ForC;
                Agent invisible = ForC.AddComponent<Agent>() as Agent;
                invisible.extRadius=1f;
                invisible.intRadius=1f;
                a.form = true;
                i++;
            }
        }
        UpdateSlots();
    }
    

    public void ActualizaPuestos() {
        for (int i = 0; i <  asignaciones.Count; i++) {
            if(asignaciones[i] == null){
                asignaciones[i] = asignaciones[i+1];
            }
            else
                asignaciones[i] = asignaciones[i];
        }
    }

 
    public bool AddCharacter(AgentNPC c) {
        // Check if the pattern supports more slots
        int occupiedSlots = _slotAssignments.Count;
        if (SupportsSlots(occupiedSlots + 1)) {
            // Add a new slot assignment
            SlotAssignment slotAssignment = new SlotAssignment();
            slotAssignment.Character = character;
            _slotAssignments.Add(slotAssignment);
            UpdateSlotAssignments();
            return true;
        } else {
            // Otherwise we've failed to add the character
            return false;
        }
    }

    public void RemoveCharacter(AgentNPC c) {
        foreach (AgentNPC a in asignaciones) {
            if (c.Equals(a))
                asignaciones.Remove(a);
        }
        UpdateSlotAssignments();
    }


    public abstract void UpdateSlots();


    public abstract Location GetSlotLocation(int slotNumber);


    public abstract bool SupportsSlots(int slotCount);
    */
}

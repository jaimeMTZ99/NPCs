using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Estado : MonoBehaviour
{

    public NPC npcObjetivo;         //el objetivo cuando realice algun ataque
    public bool move = false;          //para indicar si se esta moviendo 

    public abstract void Accion(NPC n);
    public abstract void EntrarEstado(NPC n);
    public abstract void SalirEstado(NPC n);
}

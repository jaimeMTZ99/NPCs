using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] 
    private float radio; 
    [SerializeField] public List<GameObject> nodos = new List<GameObject>();
         
    public void AppendPointToPath(GameObject go) {
            nodos.Add(go);
    }

    public void ClearPath() {       // limpiar el camino
        foreach (GameObject n in nodos){
            Destroy(n);
        }
        nodos.Clear();
    }

    public int Length() {           //numero nodos del camino
        return nodos.Count;
    }

    public float Radio{
        get => radio;
        set {
            radio = value;
        }
    }
    public Vector3 GetPosition(int valor) {
        // si el camino esta vacio pues devolvemos la misma posicion
            if (nodos.Count == 0)
                return transform.position;
            
            // Devuelve el ultimo punto si no hay mas despues
            if (valor >= nodos.Count){
                return nodos[nodos.Count - 1].transform.position;
            }

            // Devuelve el primer punto si no hay mas antes de el
            if (valor < 0)
                return nodos[0].transform.position;


            return nodos[valor].transform.position;

    }
    public bool EndOfThePath(int currentPosition){
        return currentPosition >= nodos.Count;
    }
    public int GetParam(Vector3 characterPosition, int currentPosition) {

            // si no hay camino , devuelve la misma posicion
            if (nodos.Count <= 1) {
                return currentPosition;
            }
            // If current position is somehow outside the path, return the last point
            if (currentPosition >= nodos.Count) {
                return nodos.Count - 1;
            }
            
            // encontramos el punto mas cercano, el siguiente y el anterior
            float distanceToPreviousPoint, distanceToCurrentPoint, distanceToNextPoint; // a, b, c

            // principio, no hay anteriores
            if (currentPosition == 0) {
                distanceToPreviousPoint = float.MaxValue;
                distanceToNextPoint = Vector3.Distance(characterPosition, nodos[currentPosition + 1].transform.position);
            }
            // final, no hay posteriores
            else if (currentPosition == nodos.Count - 1) {
                distanceToPreviousPoint = Vector3.Distance(characterPosition, nodos[currentPosition - 1].transform.position);
                distanceToNextPoint = float.MaxValue;
            }
            else {
                distanceToNextPoint = Vector3.Distance(characterPosition, nodos[currentPosition + 1].transform.position);
                distanceToPreviousPoint = Vector3.Distance(characterPosition, nodos[currentPosition - 1].transform.position);
            }

            distanceToCurrentPoint = Vector3.Distance(characterPosition, nodos[currentPosition].transform.position);


            if (distanceToCurrentPoint <= distanceToPreviousPoint && distanceToCurrentPoint <= distanceToNextPoint){
                return currentPosition;
            }

            if (distanceToNextPoint <= distanceToPreviousPoint && distanceToNextPoint < radio){
                return currentPosition + 1;
            }


            return currentPosition;
        }
}


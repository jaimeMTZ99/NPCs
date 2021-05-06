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

    public void ClearPath() {
        foreach (GameObject n in nodos){
            Destroy(n);
        }
        nodos.Clear();
    }

    public int Length() {
        return nodos.Count;
    }

    public float Radio{
        get => radio;
        set {
            radio = value;
        }
    }
    public Vector3 GetPosition(int keyPointValue) {
        // Return the current position if the path is empty
            if (nodos.Count == 0)
                return transform.position;
            
            // Return the last point if further inexistent points are requested
            if (keyPointValue >= nodos.Count){
                return nodos[nodos.Count - 1].transform.position;
            }

            // Return the first point if previous inexistent points are requested
            if (keyPointValue < 0)
                return nodos[0].transform.position;


            return nodos[keyPointValue].transform.position;

    }
    public int GetParam(Vector3 characterPosition, int currentPosition) {

            // If there is no path return current position
            if (nodos.Count <= 1) {
                return currentPosition;
            }
            // If current position is somehow outside the path, return the last point
            if (currentPosition >= nodos.Count) {
                return nodos.Count - 1;
            }
            
            // Find the closest point to the path between the current position, the previous and the next one
            float distanceToPreviousPoint, distanceToCurrentPoint, distanceToNextPoint; // a, b, c

            // If we're at the start, there's no previous point
            if (currentPosition == 0) {
                distanceToPreviousPoint = float.MaxValue;
                distanceToNextPoint = Vector3.Distance(characterPosition, nodos[currentPosition + 1].transform.position);
            }
            // If we're at the end, there's no next point
            else if (currentPosition == nodos.Count - 1) {
                distanceToPreviousPoint = Vector3.Distance(characterPosition, nodos[currentPosition - 1].transform.position);
                distanceToNextPoint = float.MaxValue;
            }
            // Otherwise, both points exist
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


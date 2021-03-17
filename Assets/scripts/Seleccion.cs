using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * L. Daniel Hernández. 2018. Copyleft
 * 
 * Una propuesta para dar órdenes a un grupo de agentes sin formación.
 * 
 * Recursos:
 * Los rayos de Cámara: https://docs.unity3d.com/es/current/Manual/CameraRays.html
 * "Percepción" mediante Physics.Raycast: https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
 * SendMessage to external functions: https://www.youtube.com/watch?v=4j-lh3C_w1Q
 * 
 * */

public class Seleccion : MonoBehaviour
{
    private struct Unit {
        public Agent agent;
        public SeekAcceleration seek;
        public ArriveAcceleration arrive;
        public Outline outline;
    }

    // Inspector elements
    public List<Transform> SelectableUnits;
    public Color SelectionColor;
    public Color SelectionBorderColor;
    public float BorderThickness;
    public Collider Floor;
    
    // Private elements
    private List<Unit> selectedUnits;
    private RaycastHit hit;
    private bool isDragging = false;
    private Vector3 clickPosition;
    private bool multipleSelection = false;

    // Start is called before the first frame update
    void Start() {
        selectedUnits = new List<Unit>();
    }

    // Update is called once per frame
    void Update() {
        EntradaRaton();
    }
    private void EntradaRaton() {
        multipleSelection = Input.GetKey(KeyCode.LeftShift); // Left SHIFT for multiple selection

        // Left click to select units
        if (Input.GetMouseButtonDown(0)) {

            // Update elements
            clickPosition = Input.mousePosition;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cameraRay, out hit)) {
                // If so, add it to the selected units list
                if (hit.transform.CompareTag("Unit")) {
                    SeleccionarNPC(hit.transform, multipleSelection);
                } else {
                    // Otherwise, begin drag selection
                    isDragging = true;
                }
            }
        } else if (Input.GetMouseButtonUp(0) && isDragging) {
            // Clear the previous selection if SHIFT isn't being pressed down
            if (!multipleSelection) {
                QuitarNPCSeleccionados();
            }

            // Check if any of our selectable units is within the rectangle selection
            foreach (Transform unit in SelectableUnits) {
                // Ignore disabled objects
                if (!unit.gameObject.activeSelf)
                    continue;
                
                if (IsWithinSelectionBounds(unit))
                    SeleccionarNPC(unit, true);
            }
            isDragging = false; 
        }
        // Right click while having selected units to make them go the right click position
        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0) {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Floor.Raycast(cameraRay, out hit, 1000)) {

                // Ignore the Y Axis
                Vector3 twoAxis = hit.point;
                twoAxis.y = 0.5f;

                foreach (Unit unit in selectedUnits) {
                    if (unit.seek != null) {
                        unit.seek.target.Position = twoAxis;
                    }
                    if (unit.arrive != null) {
                        unit.arrive.target.Position = twoAxis;
                    }
                    /*if (unit.pathFinding != null) {
                        unit.pathFinding.FindPathToPosition(unit.agent.Position, twoAxis);
                    }*/
                }
            }
        }

    }
    private void SeleccionarNPC(Transform target, bool multipleSelection) {
        // Reset the current selection if SHIFT isn't being pressed down and something is already selected
        if (!multipleSelection && selectedUnits.Count > 0) {
            QuitarNPCSeleccionados();
        }

        // Enable the outline highlight
        Outline outline = target.GetComponent<Outline>();
        if (outline.enabled)
            return; // Unit already selected
        outline.enabled = true;

        // Very inefficient but it is only used for the demo scenes
        Unit unit = new Unit();
        unit.outline = outline;
        unit.seek = target.GetComponent<SeekAcceleration>();
        unit.arrive = target.GetComponent<ArriveAcceleration>();
       // unit.pathFinding = target.GetComponent<PathFinding>();
        unit.agent = target.GetComponent<Agent>();

        selectedUnits.Add(unit);
    }
    private void QuitarNPCSeleccionados() {
        foreach (Unit unit in selectedUnits) {
            unit.outline.enabled = false;
        }
        selectedUnits.Clear();
    }

    /*private void OnGUI() {
        // Draw the rectangle on dragging
        if (isDragging) {
            Rect rect = ScreenHelper.GetScreenRect(_clickPosition, Input.mousePosition);
            ScreenHelper.DrawScreenRect(rect, SelectionColor);
            ScreenHelper.DrawScreenRectBorder(rect, BorderThickness, SelectionBorderColor);
        }
    }*/
    
    // Helper function to check whether a transform is within our rectangle selection or not
    private bool IsWithinSelectionBounds(Transform transform) {
        if (!isDragging)
            return false;
        
        Camera cam = Camera.main;
        //Bounds bounds = ScreenHelper.GetViewportBounds(cam, clickPosition, Input.mousePosition);
        //return bounds.Contains(cam.WorldToViewportPoint(transform.position));
        return false;
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private float movementVelocity = 0.25f;
    private Camera camera;
    private float horizontalMovement;
    private float verticalMovement;

    void Start() {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {

        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // Simple horizontal and vertical movement
        if (horizontalMovement != 0 || verticalMovement != 0) {
            Vector3 position = transform.localPosition;

            position.x += horizontalMovement * movementVelocity;
            position.z += verticalMovement *movementVelocity;
            transform.localPosition = position;
        }
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;               // Reference to the Rigidbody component
    public float moveSpeed = 5f;        // Speed of the player movement
    public float mouseSensitivity = 100f; // Sensitivity for camera movement
    public Transform cameraTransform;  // Reference to the player's camera

    private float xRotation = 0f;      // For vertical camera rotation
    private Vector3 moveDirection;     // To store movement input

    void Start()
    {
        // Lock the cursor to the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle camera movement with mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player horizontally based on mouse X movement
        transform.Rotate(Vector3.up * mouseX);

        // Handle vertical camera rotation (clamped to prevent over-rotation)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to the camera
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Get movement input (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down Arrow

        // Calculate movement direction based on camera's orientation
        moveDirection = cameraTransform.right * moveX + cameraTransform.forward * moveZ;

        // Ensure movement stays on the horizontal plane (zero Y-axis movement)
        moveDirection.y = 0f;
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody's velocity
        Vector3 move = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
}

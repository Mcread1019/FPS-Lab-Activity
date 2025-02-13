using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Fields
    [Header("Player Stats")]
    [Tooltip("Movement speed of the player in meters per second.")]
    [SerializeField] float speed;
    [Tooltip("Camera look sensitivity.")]
    [SerializeField] float sensitivity;
    [Tooltip("Player sprint speed in meters per second.")]
    [SerializeField] float sprintSpeed;

    [SerializeField] float jumpForce;
    [SerializeField] float gravity = 9.81f;


    // Used to store the forward and backward movement input.
    private float moveFB;
    // Used to store the right and left movement input.
    private float moveLR;
    // Used to store the mouse right and left input.
    private float rotX;
    // Used to store the mouse up and down input.
    private float rotY;

    // References
    // Reference to the player's vision camera.
    private Camera playerCam;
    // Reference to the CharacterController component on the Player.
    private CharacterController cc;

    void Start()
    {
        // Locks the cursor inside of the game window.
        // Additionally by default, it also hides the cursor, making it not visible.
        Cursor.lockState = CursorLockMode.Locked;
        // Get the reference to the CharacterController component on the Player.
        cc = GetComponent<CharacterController>();
        // Access the first child of the Player and get the Camera component from it.
        playerCam = transform.GetChild(0).GetComponent<Camera>();
    }

    void Update()
    {
        // Check every frame for movement input and apply the movement.
        Move();
    }

    // This method handles all player movement input and moves the Player accordingly.
    private void Move()
    {
        // Local variable to keep track of the current movement speed.
        float movementSpeed = speed;

        // Check to see if the Left Shift key is being held down.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // The player is sprinting, so change the movementSpeed variable to the sprintSpeed.
            movementSpeed = sprintSpeed;
        }
        // Redundant check to see if the Left Shift key is NOT being held down.
        else
        {
            // The player is NOT sprinting, so change the movementSpeed variable to the normal speed.
            movementSpeed = speed;
        }


        // Get the forward/backward movement input for direction, and apply the speed.
        moveFB = Input.GetAxis("Vertical") * movementSpeed;
        // Get the right/left movement input for direction, and apply the speed.
        moveLR = Input.GetAxis("Horizontal") * movementSpeed;
        // Get the right/left mouse movement for direction, and apply the sensitivity.
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        // Get the up/down mouse movement for direction, apply the sensitivity, and subtract it from the previous rotY value.
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Clamp the value of rotY between -60 degrees and +60 degrees.
        rotY = Mathf.Clamp(rotY, -60f, 60f);

        // Calculate the movement vector for the player by applying forward/backward movement and right/left movement.
        // Notice we normalize the vector making it have a magnitude of 1. This essentially makes it a direction only vector, with no distance (speed).
        // Finally, we multiply by the movementSpeed to get our distance.
        Vector3 movement = new Vector3(moveLR, 0, moveFB).normalized * movementSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
        {
            cc.Move(Vector3.up * jumpForce);
        }

        if (cc.isGrounded)
        {
            movement.y = -2f;
        }
        else
        {
            movement.y -= gravity;
        }

        // Use the right/left mouse movement to rotate the Player's body left and right (around the Y axis).
        transform.Rotate(0, rotX, 0);

        // Rotate the Player's camera up and down (on the X axis) according to up/down mouse movement.
        playerCam.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        // Update our movement vector to take into account the current Player's rotation, and combine that with the current movement vector.
        movement = transform.rotation * movement;
        // Call the Move() method on the CharacterController component and pass in our movement vector, not forgetting to multiple by Time.deltaTime.
        // Remember multiplying by Time.deltaTime is necessary to keep framerate independent movement speeds.
        cc.Move(movement * Time.deltaTime);
    }
}
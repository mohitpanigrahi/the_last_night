using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Speeds")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2f;

    [Header("Physics & Gravity")]
    private float gravity = -9.81f;
    private Vector3 velocity;
    
    // Reference to Unity's CharacterController component
    private CharacterController controller;

    void Start()
    {
        // Automatically find the CharacterController component on this player object
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get player input from WASD / Arrow keys
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Determine movement direction relative to where the player is facing
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Determine current speed based on input (Sprinting with Left Shift, Sneaking with Left Ctrl)
        float currentSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed; // Fast run for escaping guards
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = crouchSpeed; // Slow stealth crawl for avoiding warden
        }

        // Move the player
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Apply basic gravity so the player stays grounded
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
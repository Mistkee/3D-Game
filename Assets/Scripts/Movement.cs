using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed = 5f;
    public float JumpForce = 8f;
    public float Gravity = 9.8f;
    private float velocity = 0;
    private bool isJumping = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Gravity
        if (characterController.isGrounded && velocity < 0)
        {
            velocity = -0.5f;
        }
        else
        {
            velocity -= Gravity * Time.deltaTime;
        }

        // Camera
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;

        Vector3 moveDirection = (cameraRight * horizontal + cameraForward * vertical).normalized * MovementSpeed;

        // Jump
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity = JumpForce;
                isJumping = true;
            }
        }

        characterController.Move((moveDirection + new Vector3(0, velocity, 0)) * Time.deltaTime);

        if (characterController.isGrounded && isJumping)
        {
            isJumping = false;
        }
    }
}



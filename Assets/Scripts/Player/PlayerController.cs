using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput1 playerInput;
    [SerializeField, Range(0f,15f)] float moveSpeed;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private Vector2 move;
    [SerializeField, Range(0, 3f)] float jumpHeight;
    private CharacterController controller;

    [SerializeField] Transform ground;
    [SerializeField] float distanceToGround;
    [SerializeField] LayerMask whatIsGround;
    private bool isGrounded;

    private bool isShooting;

    private void Awake()
    {
        playerInput = new PlayerInput1();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, whatIsGround);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump(context);
    }

    private void PlayerMovement()
    {
        //move = context.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, whatIsGround);

        if (context.performed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        PlayerMovement();
    }
}

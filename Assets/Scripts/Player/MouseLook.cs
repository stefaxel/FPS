using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    private PlayerInput1 playerInput;
    [SerializeField, Range(0f, 100f)] float mouseSensitivity;
    private Vector2 mouseLook;
    private float yRotation = 0f;
    [SerializeField] Transform playerBody;

    private void Awake()
    {
        playerInput = new PlayerInput1();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CursorPosition();
    }

    private void CursorPosition()
    {
        mouseLook = playerInput.Player.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}

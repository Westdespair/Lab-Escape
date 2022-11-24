using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Tooltip("The camera that will be manipulated by the script")]
    public GameObject playerCamera;

    [Tooltip("The horizontal aim sensitivity.")]
    public float horizontalSensitivity;

    [Tooltip("The vertical aim sensitivity.")]
    public float verticalSensitivity;

    [Tooltip("If the x-axis should be inverted at starttime.")]
    public bool invertX = false;

    [Tooltip("If the y-axis should be inverted at starttime.")]
    public bool invertY = false;

    private float verticalRotation;
    private float horizontalRotation;
    private PlayerInput playerInput;
    private InputAction look;
    float xRotation = 0f;
    public Transform playerbody;
    public float mouseSense = 100f;


    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        look = playerInput.PlayerController.Look;
        look.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (invertX)
        {
            horizontalSensitivity *= (-1);
        }

        if (invertY)
        {
            verticalSensitivity *= (-1);
        }

        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 lookInput = look.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSense * Time.deltaTime;
        float mouseY = lookInput.y * mouseSense * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerbody.Rotate(Vector3.up * mouseX);
    }
}

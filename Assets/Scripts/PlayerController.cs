using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    [Tooltip("Movement speed of player in m/s")]
    [SerializeField] public float playerSpeed = 10.0f;
    [Tooltip("Jump height of player")]
    public float jumpHeight = 10.0f;
    public float rotationSpeed;
    public float dashForce;
    public Rigidbody rb;
    private InputAction moveDirection;
    private InputAction jump;
    private InputAction dash;
    private PlayerInput playerInput;
    private bool permissionToJump = true;
    public GameObject weapon;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        moveDirection = playerInput.PlayerController.Movement;
        moveDirection.Enable();

        jump = playerInput.PlayerController.Jump;
        jump.Enable();
        jump.performed += OnJump;

        dash = playerInput.PlayerController.Dash;
        dash.Enable();
        dash.performed += OnDash;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        MoveplayerRelativeToCamera();
    }

    void MoveplayerRelativeToCamera()
    {
       Vector2 movement = GetMoveDirection();
       Vector3 cameraRelativeMovement = WorldVector3ToLocalCameraVector2(movement) * playerSpeed * Time.deltaTime;
        rb.AddForce(cameraRelativeMovement, ForceMode.VelocityChange);
 
    }

    void OnCollisionEnter(Collision collision)
    {
        permissionToJump = true;
    }

    void OnCollisionExit(Collision collision)
    {
        permissionToJump = false;
    }

    //private bool isOnGround()
    //{
    //return Physics.CheckCapsule(new Vector3(playerCollider.center.x, playerCollider.bounds.min.y, playerCollider.center.z)
    //, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.25f, playerCollider.bounds.center.z)
    //, playerCollider.radius, LayerMask.NameToLayer("Player"));

    //}
    private void OnJump(InputAction.CallbackContext context)
    {
        TryJump();
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        TryDash();
    }

    private void TryJump()
    {
        if (permissionToJump)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }

    /**
     * Attempts to perform a dash. If the player is allowed to, dash in the current direction the player is moving. 
     * TODO: Add cooldown of sorts.
    */
    private void TryDash()
    {
        Vector3 cameraRelativeMovement = WorldVector3ToLocalCameraVector2(GetMoveDirection()) * dashForce;
        rb.AddForce(cameraRelativeMovement, ForceMode.VelocityChange);
        Debug.Log("Dashing");
    }

    /**
     * Returns a vector 2 representing the players vertical and horizontal moveinput. 
     */
    private Vector2 GetMoveDirection()
    {
        return moveDirection.ReadValue<Vector2>();
    }


    /**
     * Converts a vector 2 in world space to a vector 3 pointed towards where the camera is pointing. 
     */
    private Vector3 WorldVector3ToLocalCameraVector2(Vector2 vectorToConvert)
    {
        float convertedX = vectorToConvert.x;
        float convertedY = vectorToConvert.y;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeInwardInput = convertedY * forward;
        Vector3 rightRelativeHorizontalInput = convertedX * right;
       
        return forwardRelativeInwardInput + rightRelativeHorizontalInput;
    }
}
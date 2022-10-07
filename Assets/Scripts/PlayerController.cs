using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    [Tooltip("Movement speed of player in m/s")]
    [SerializeField] public float playerSpeed = 10.0f;
    private bool permissionToJump = true;
    [Tooltip("Jump height of player")]
    public float jumpHeight = 10.0f;
    public float rotationSpeed;
    public Rigidbody rb;
 
    
    // Start is called before the first frame update

    private CharacterController characterController;

    void Start()
    {
       
    }



    // Update is called once per frame
    void Update()


    {

        if (Input.GetButtonDown("Jump") && permissionToJump) {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }

        MoveplayerRelativeToCamera();



        //float xMove = Input.GetAxis("Horizontal");
        //float yMove = Input.GetAxis("Vertical");
        //rb.AddForce(new Vector3(xMove, 0, yMove)* playerSpeed * Time.deltaTime);
        //Vector3 movementDirection = new Vector3(xMove, 0, yMove);
        //transform.Translate(movementDirection * playerSpeed * Time.deltaTime, Space.World);

        //if(movementDirection != Vector3.zero)
        //{
           // Quaternion toRotation = Quaternion.LookRotation(movementDirection);
           // transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        //}
    }

    void MoveplayerRelativeToCamera()
    {
    

        float playerVerticalInput = Input.GetAxis("Vertical");
        float playerHorizontalInput = Input.GetAxis("Horizontal");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = playerVerticalInput * forward * playerSpeed;
        Vector3 rightRelativeHorizontalInput = playerHorizontalInput * right;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;

        this.transform.Translate(cameraRelativeMovement, Space.World);
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



}

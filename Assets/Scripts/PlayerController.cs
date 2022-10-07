using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    [Tooltip("Movement speed of player in m/s")]
    public float playerSpeed = 100f;
    private bool permissionToJump = true;
    [Tooltip("Jump height of player")]
    public float jumpHeight = 2f;
    public Rigidbody rb;
 
    
    // Start is called before the first frame update

    private CharacterController characterController;
    private PlayerController playerController;

    void Start()
    {
        SetupCharacterContoller();
    }

    void SetupCharacterContoller()
    {
        characterController = GetComponent<CharacterController>();
        if(characterController == null)
        {
            Debug.LogError("The player character does not have a character controller on the same game object");
        }
    }

    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetButtonDown("Jump") && permissionToJump) {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
        
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        //rb.AddForce(new Vector3(xMove, 0, yMove)* playerSpeed * Time.deltaTime);
        Vector3 movementDirection = new Vector3(xMove, 0, yMove);
        transform.Translate(movementDirection * playerSpeed * Time.deltaTime, Space.World);

    }


    private void OnCollisionEnter(Collision collision)
    {
        permissionToJump = true;
    }

    private void OnCollisionExit(Collision collision)
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

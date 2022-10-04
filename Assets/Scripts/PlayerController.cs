using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [Tooltip("Movement speed of player in m/s")]
    public float playerSpeed;
    private bool permissionToJump = true;
    [Tooltip("Jump height of player")]
    public float jumpHeight;
    public Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    private bool isJumpPressed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetButtonDown("Jump") && permissionToJump) {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }



        
        

        
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        rb.AddForce(new Vector3(xMove, 0, yMove)* playerSpeed * Time.deltaTime);  

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

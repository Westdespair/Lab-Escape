using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction primaryAttack;
    private InputAction secondaryAttack;
    private InputAction interact;
    private bool fireIsPressed;
    [Tooltip("The weapon held by the player.")]
    public GameObject weaponSlot;
    public GameObject pickupCollider;
    public GameObject projectile;
    public GameObject clawSlot;
    private Weapon weaponScript;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        primaryAttack = playerInput.PlayerController.PrimaryAttack;
        primaryAttack.Enable();
        primaryAttack.performed += OnPrimaryAttack;
        primaryAttack.performed += _ => fireIsPressed = true;
        primaryAttack.canceled += _ => fireIsPressed = false;

        interact = playerInput.PlayerController.Interact;
        interact.Enable();
        interact.performed += OnInteract;

        secondaryAttack = playerInput.PlayerController.SecondaryAttack;
        secondaryAttack.Enable();
        secondaryAttack.performed += OnSecondaryAttack;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponSlot != null)
        {
            if(fireIsPressed && weaponSlot.GetComponent<Weapon>().mode == Weapon.FireMode.Auto)
            {
                weaponSlot.GetComponent<Weapon>().FireWeapon();
            }
        }
    }

    private void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        // Instantiate<GameObject>(projectile,transform.position,transform.rotation);
        Debug.Log("Primary attack input has been pressed");
        weaponSlot.GetComponent<Weapon>().FireWeapon();
    }

    private void OnSecondaryAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Secondary attack input has been pressed");
        clawSlot.GetComponent<PlayerMeleeController>().ClawAttack();

    }

    private void OnInteract(InputAction.CallbackContext context) {
        Debug.Log("interact!");
        GameObject pickupObject = pickupCollider.GetComponent<InteractablesWithinCollider>().getFirstInteractable();
        if (pickupObject != null && pickupObject.GetComponent<Interactable>().pickUpable == true) {
            weaponSlot = pickupObject;
            pickupObject.transform.SetParent(gameObject.transform);
            weaponSlot.transform.localPosition = weaponSlot.GetComponent<Weapon>().basePosition;
            weaponSlot.transform.localEulerAngles = new Vector3(-90,180,0);//weaponSlot.GetComponent<Weapon>().baseRotation;
            weaponSlot.GetComponent<Weapon>().resetBasePosition();
            //Disable unwanted components while in hand
            weaponSlot.GetComponent<Rigidbody>().isKinematic = true;
            weaponSlot.GetComponent<Collider>().enabled = false;
        }
    }
}
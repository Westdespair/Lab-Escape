using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction primaryAttack;
    private InputAction secondaryAttack;
    private InputAction throwItem;
    private InputAction interact;
    private bool fireIsPressed;
    [Tooltip("The weapon held by the player.")]
    public GameObject weaponSlot;
    public GameObject pickupCollider;
    public GameObject projectile;
    public GameObject claw;
    private Weapon weaponScript;
    public bool canAttack = true;
    public float attackCooldown = 1.0f;
    public bool isAttacking = false;


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

        throwItem = playerInput.PlayerController.Throwweapon;
        throwItem.Enable();
        throwItem.performed += OnThrowInput;

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
            if(fireIsPressed && weaponSlot.GetComponent<Weapon>().mode == Weapon.FireMode.FullAuto)
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
        ClawAttack();

    }

    public void ClawAttack()
    {
        if (isAttacking == false)
        {
            GameObject.Find("Monster hand").GetComponent<Animation>().Play();
            isAttacking = true;
            canAttack = false;
            StartCoroutine(ResetAttackCooldown());
        }

    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
    private void OnInteract(InputAction.CallbackContext context) {
        Debug.Log("interact!");
        GameObject pickupObject = pickupCollider.GetComponent<InteractablesWithinCollider>().getFirstInteractable();
        if (pickupObject != null && pickupObject.GetComponent<Interactable>().pickUpable == true) {
            weaponSlot = pickupObject;
            Weapon weaponScript = weaponSlot.GetComponent<Weapon>();
            Collider weaponCollider = weaponSlot.GetComponent<Collider>();

            pickupObject.transform.SetParent(gameObject.transform);
            weaponSlot.transform.localPosition = weaponSlot.GetComponent<Weapon>().basePosition;
            weaponSlot.transform.localEulerAngles = new Vector3(-90,180,0);
            weaponSlot.GetComponent<Weapon>().resetBasePosition();
            //Disable unwanted components while in hand
            weaponScript.SetMode(Weapon.WeaponMode.InPlayerHand);
        }
    }
    
    private void OnThrowInput(InputAction.CallbackContext context)
    {
        Debug.Log("Throwing!");
        weaponSlot.GetComponent<Weapon>().SetMode(Weapon.WeaponMode.Thrown);
        float throwForce = 15;
        weaponSlot.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * throwForce, ForceMode.VelocityChange);
        weaponSlot = null;
        AudioManager.instance.ActionSFX(0);
    }
}
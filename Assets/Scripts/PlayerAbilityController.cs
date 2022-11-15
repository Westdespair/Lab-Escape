using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction primaryAttack;
    private InputAction secondaryAttack;
    private bool fireIsPressed;
    public GameObject weaponSlot;
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
}
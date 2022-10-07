using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction primaryAttack;
    private InputAction secondaryAttack;
    private GameObject weaponSlot;
    public GameObject projectile;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        primaryAttack = playerInput.PlayerController.PrimaryAttack;
        primaryAttack.Enable();
        primaryAttack.performed += OnPrimaryAttack;

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
        
    }

    private void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        Instantiate<GameObject>(projectile,transform.position,transform.rotation);
    }

    private void OnSecondaryAttack(InputAction.CallbackContext context)
    {

    }
}
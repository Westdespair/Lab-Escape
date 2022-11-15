using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawCollissionEnter : MonoBehaviour
{

    public PlayerAbilityController playerMelee;
    private int damageAmount = 1;
    private Damage damage;


    private void Start()
    {
        damage = gameObject.GetComponent<Damage>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Object") && playerMelee.isAttacking)
        {
         
            Debug.Log(other.name);
            damage.DealDamage(damageAmount, other.gameObject);
        }
    }
}

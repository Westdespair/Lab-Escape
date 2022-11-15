using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeController : MonoBehaviour
{

    public GameObject claw;
    public bool canAttack = true;
    public float attackCooldown = 1.0f;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (canAttack)
            {
                ClawAttack();
            }
        }
    }



    public void ClawAttack()
    {
        isAttacking = true;
        canAttack = false;
        StartCoroutine(ResetAttackCooldown());
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollision : MonoBehaviour
{
    private Collider damageCollider;
    private Damage damage;
    public int damageAmount;
    // Start is called before the first frame update
    void Start()
    {
        damageCollider = gameObject.GetComponent<Collider>();
        damage = gameObject.GetComponent<Damage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Deals damage to the object it collides with if it has a health component. 
    private void OnCollisionEnter(Collision collision)
    {
        damage.DealDamage(damageAmount,collision.gameObject);
    }
}

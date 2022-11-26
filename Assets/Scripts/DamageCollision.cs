using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollision : MonoBehaviour
{
    private Collider damageCollider;
    private Damage damage;
    public int damageAmount = 1;

    private Vector3 currentPosition;
    private Vector3 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        damageCollider = gameObject.GetComponent<Collider>();
        damage = gameObject.GetComponent<Damage>();
        currentPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    //Creates a line that targets the positions between frames for super fast damage dealers.
    void Update()
    {
        previousPosition = currentPosition;
        currentPosition = gameObject.transform.position;
        RaycastHit hit;
        if (Physics.Linecast(currentPosition, previousPosition, out hit)) {
            damage.DealDamage(damageAmount, hit.collider.gameObject);
        }
    }

    //Deals damage to the object it collides with if it has a health component. 
    private void OnCollisionStay(Collision collision)
    {
        damage.DealDamage(damageAmount,collision.gameObject);
    }
}

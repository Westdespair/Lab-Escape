using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Deal damage to a gameObject, given that it has a health component attached. 
     */
    public void DealDamage(int damage, GameObject target)
    {
        if (target.GetComponent("Health") != null)
        {
            target.GetComponent<Health>().TakeDamage(damage);
        };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjects : MonoBehaviour
{
    int damageObject = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(int damage, GameObject target)
    {
        if (target.GetComponent("Health") != null)
        {
            target.GetComponent<DestroyWallLamp>().TakeDamage(damage);

        };
    }

    


}

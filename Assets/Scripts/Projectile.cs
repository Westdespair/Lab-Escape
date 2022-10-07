using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float movementSpeed;
    public GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate<GameObject>(projectile,gameObject.transform);
        Rigidbody projectileRb= projectile.GetComponent<Rigidbody>();
        if(projectileRb != null && !projectileRb.isKinematic)
        {
            projectileRb.velocity = (Vector3.forward * movementSpeed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

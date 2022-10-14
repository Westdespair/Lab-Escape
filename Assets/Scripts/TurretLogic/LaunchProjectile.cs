using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private float launchSpeed = 10000f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            GameObject launchedObject = Instantiate(projectile, transform.position, transform.rotation);
            launchedObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0,0 , -launchSpeed));
        }
        
    }
}

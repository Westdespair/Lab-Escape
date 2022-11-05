using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    public bool usesRigidBody = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        if (usesRigidBody && rigidbody != null) {
            rigidbody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        }   
    }
    // Update is called once per frame
    void Update()
    {
        if (!usesRigidBody) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}

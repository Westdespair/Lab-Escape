using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRigidBody : MonoBehaviour
{
    public Vector3 spin;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        if (rigidbody != null) {
            rigidbody.AddRelativeTorque(spin, ForceMode.VelocityChange);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

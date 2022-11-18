using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Spins a rigid body by a vector 3.
 */
public class SpinRigidBody : MonoBehaviour
{
    public Vector3 spin;

    // Start is called before the first frame update
    //Applies the spin vector to the rigidbody as a velocitychange
    void Start()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        if (rigidbody != null) {
            rigidbody.AddRelativeTorque(spin, ForceMode.VelocityChange);
        }
    }
}

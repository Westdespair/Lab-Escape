
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int damage = 1;
    public float range = 100f;
    public Camera fpsCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name);
            Health objectTarget = hit.transform.GetComponent<Health>();
       
            if (objectTarget != null)
            {
                objectTarget.TakeDamage(damage);
            }
        }
    }
}

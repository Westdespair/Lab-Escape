
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 1f;
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
            ObjectTarget objectTarget = hit.transform.GetComponent<ObjectTarget>();
            EnemyTarget enemy = hit.transform.GetComponent<EnemyTarget>();
            if (objectTarget != null)
            {
                objectTarget.TakeDamage(damage);
            }
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}

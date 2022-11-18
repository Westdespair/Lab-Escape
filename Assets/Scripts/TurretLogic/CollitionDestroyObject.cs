using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollitionDestroyObject : MonoBehaviour
{

    public GameObject spawnObject;
    public GameObject destroyGameObject;

    private void OnCollisionEnter(Collision collision) 
    {
        Instantiate(spawnObject, transform.position, transform.rotation);


        GameObject.Destroy(destroyGameObject);
    }
}

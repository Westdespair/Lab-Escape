using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWallLamp : MonoBehaviour
{

    public GameObject spawnObject;
    public GameObject destroyGameObject;
    public int currentHealth;
    public int maxHealth;
    public bool canDie;




    private void Update()
    {
        if (currentHealth < 1)
        {
            DestroyObject();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("HP left: " + currentHealth);
    }

    public void DestroyObject()
    {
        Instantiate(spawnObject, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }


    
}

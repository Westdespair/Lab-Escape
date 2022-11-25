using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool canDie;
    public GameObject spawnObject;
    public GameObject destroyGameObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnObject == null && destroyGameObject == null)
        {
            if ((currentHealth < 1 && CompareTag("Enemy")) || (currentHealth < 1 && CompareTag("Player")))
            {
                OnDeathEnemy();
            }
        }
        if (currentHealth < 1 && CompareTag("Object"))
        {
            OnDestroyObject();
        }


    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("HP left: " +  currentHealth);
    }

    public void OnDeathEnemy()
    {
        if (CompareTag("Enemy"))
        {
            gameObject.GetComponent<SmarterAI>().die();
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void OnDestroyObject()
    {
        Instantiate(spawnObject, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}

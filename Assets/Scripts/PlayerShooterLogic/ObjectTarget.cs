using UnityEngine;

public class ObjectTarget : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject destroyGameObject;

    public float health = 2f;
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0f)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        Instantiate(spawnObject, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}

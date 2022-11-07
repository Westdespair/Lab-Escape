using UnityEngine;

public class EnemyTarget : MonoBehaviour
{

    public float health = 5f;
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
        Destroy(this.gameObject);
    }
}

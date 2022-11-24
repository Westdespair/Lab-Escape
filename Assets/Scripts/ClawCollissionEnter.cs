using UnityEngine;

public class ClawCollissionEnter : MonoBehaviour
{

    public PlayerAbilityController playerMelee;
    private int damageAmount = 1;
    private Damage damage;
    public Collider col;


    private void Start()
    {
        damage = gameObject.GetComponent<Damage>();
    }

    private void OnTriggerStay(Collider col)
    {
        if((col.CompareTag("Enemy") && playerMelee.isAttacking) || (col.CompareTag("Object") && playerMelee.isAttacking)) { 
            Debug.Log(col.name);
            damage.DealDamage(damageAmount, col.gameObject);
        }
    }
}

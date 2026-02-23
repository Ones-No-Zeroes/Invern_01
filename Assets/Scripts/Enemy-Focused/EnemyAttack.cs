using Unity.Mathematics;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Enemy Attack Attributes")]
    [SerializeField] private int horizontalKnockBackForce;
    [SerializeField] private int verticalKnockBackForce;
    [SerializeField] private int damageAmount;
    [SerializeField] private LayerMask playerLayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            float dot = Vector2.Dot(transform.position, collision.gameObject.transform.position);
            
            collision.GetComponent<PlayerHealth>().DamageCharacter(damageAmount); 
            collision.GetComponent<PlayerKnockbackLogic>().EnableKnockBack(math.sign(dot * -1),horizontalKnockBackForce, verticalKnockBackForce); // Currently is not ideal because the direction of the player never changes... Needs to be fixed
        }
    }
}

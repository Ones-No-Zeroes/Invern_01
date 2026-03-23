using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(BossHealth))]
public class JumpOnEnemyHead : MonoBehaviour
{
    // Private Variables
    private BossHealth bossHealth;

    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.TryGetComponent(out PlayerKnockbackLogic player)) // Get the PlayerController, try-get
        {
            Vector2 contactNormal = other.contacts[0].normal;

            if(contactNormal.y <= -0.5f) // If it is less than or equal to -0.5, then the contact point was from the top.
            {
               Debug.Log("Hit Boss' Head");
               player.EnableKnockBack(math.sign(player.transform.localScale.x), 10, 20);
               bossHealth.DamageCharacter(1);
            }
        }
    }
   
}

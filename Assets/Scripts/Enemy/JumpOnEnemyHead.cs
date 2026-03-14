using UnityEngine;

public class JumpOnEnemyHead : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.TryGetComponent(out PlayerController player)) // Get the PlayerController, try-get
        {
            Vector2 contactNormal = other.contacts[0].normal;

            if(contactNormal.y <= -0.5f) // If it is less than or equal to -0.5, then the contact point was from the top.
            {
               Debug.Log("Hit Boss' Head");
            }
        }
    }
   
}

using System.Collections;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Player Detection Attributes")]
    [SerializeField] private Enemy enemyLogic;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private int chaseTimer;


    // Private fields
    private GameObject player;

    // Would prefer this to be a PhysicsRaycast rather than a trigger....
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!enemyLogic.isPlayerDetected && collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            enemyLogic.isPlayerDetected = true;
            StartCoroutine(IsPlayerStillInRadius());
        }
    }

    // Checks to see if the Player is still in the detection radius of the enemy
    IEnumerator IsPlayerStillInRadius()
    {
        yield return new WaitForSeconds(chaseTimer);
        if (!boxCollider.OverlapPoint(player.transform.position))
        {
            enemyLogic.isPlayerDetected = false;
        }

    }
   
}

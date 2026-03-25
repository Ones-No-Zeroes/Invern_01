using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CheckPointLogic : MonoBehaviour
{   
    // SerializedField Fields
    [Header("Checkpoint Logic Attributes")]
    [SerializeField] private PlayerHealth playerHealth;

    // Disabled to avoid exception -- animator controller will have its own "active" tracker variable - Darrem B.
//    [SerializeField] private SpriteRenderer sprite;
    
    
 



    // MIGHT BE USED IN THE FUTURE!

    // Private Fields
    private bool isMovingPlayer = false;


    // Public Fields
    public bool isCheckPointActive = false;

    /// <summary>
    /// Method will heal the player once called.
    /// </summary>
    public void ActivateCheckPoint()
    {
        playerHealth.RestoreHealthToMax();

        // Disabled along with declaration -- Darren B.
        // sprite.color = Color.green;
        
    }

    /// <summary>
    /// Method will move the player to the checkpoint when called.
    /// </summary>
    public void MovePlayerToCheckpoint(float respawnDelay)
    {
        if (isCheckPointActive && !isMovingPlayer)
        {
            isMovingPlayer = true;
            StartCoroutine(MovePlayer(respawnDelay));
        }
        
    }

    // Start Implementation of a smooth movement from beginning to end. 
    
    IEnumerator MovePlayer(float respawnDelay)
    {
        yield return new WaitForSeconds(respawnDelay);
        playerHealth.RestoreHealthToMax();
        playerHealth.gameObject.transform.position = new (gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
        isMovingPlayer = false;
    }


}

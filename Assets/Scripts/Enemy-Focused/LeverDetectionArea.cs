using UnityEngine;

public class LeverDetectionArea : MonoBehaviour
{   
    // Private Fields
    private bool playerInLeverActivationArea = false;
    private LeverEnemy currentLeverEnemy;

    // Public Properties
    public bool PlayerInLeverActivationArea{ get {return playerInLeverActivationArea;} } // Used by the Player Controller to determine wheter to do the action if the interaction key is pressed.
    public LeverEnemy CurrentLeverEnemy { get { return currentLeverEnemy; } } // Used to trigger the ToggleLever() method on the Lever Enemy whos Collider/Trigger/ActivationArea we are in.

    // If the Player enters the Trigger Area, set the bool to true
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeverEnemy"))
        {
            playerInLeverActivationArea = true;
            currentLeverEnemy = collision.transform.parent.GetComponentInParent<LeverEnemy>();

            Debug.Log("In Lever Activation Area!");
        }
    }

    // If the Player exits the Trigger Area, set the bool to false
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeverEnemy"))
        {
            playerInLeverActivationArea = false;
            currentLeverEnemy = null; // REMOVES THE LEVER ENEMY!

            // Debugging
            Debug.Log("No longer in lever activation area....");
        }
    }
}

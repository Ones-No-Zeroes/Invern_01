 using UnityEngine;
using UnityEngine.Events;

public class LeverEnemy : MonoBehaviour
{
    [Header("Lever Enemy Attributes")]
    [SerializeField] private GameObject lightModeAssets;
    [SerializeField] private GameObject voidModeAssets;
    [SerializeField] private bool isLeverOn = false;

    [Header("Lever Enemy Events")]
    [SerializeField] private UnityEvent OnLeverActivated;
    [SerializeField] private UnityEvent OnLeverDeactivated;

    // Public Properties
    public GameObject LightModeAssets {get { return lightModeAssets; } }
    public GameObject VoidModeAssets {get { return voidModeAssets; } }

    // Public Methods

    /// <summary>
    /// Toggles the Lever. If the lever was on, then turn it off. Else, if it was off, turn it on.
    /// </summary>
    public void ToggleLever()
    {
        if (isLeverOn)
        {
            Debug.Log("Player Deactivated Lever");
            isLeverOn = false;
            //OnLeverDeactivated.Invoke();

        }
        else
        {
            Debug.Log("Player Activated Lever!");
            isLeverOn = true;
           // OnLeverActivated.Invoke();
        }
    }
}

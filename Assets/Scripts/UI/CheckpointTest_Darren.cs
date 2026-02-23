using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CheckpointTest_Darren : MonoBehaviour
{
    // Variables for active, void and tracking last update for each
    public bool isActive = false, isVoid = false, wasActive = false, wasVoid = false;
    Animator animator;
    InvernLogic invernLogic;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Update worldState from InvernLogic
        invernLogic = FindFirstObjectByType<InvernLogic>();
        isVoid = invernLogic.WorldVoid;

        // Update animator component if needed
        if (isActive == wasActive && isVoid == wasVoid)
        {
            return;
        }
        else
        {
            if (isActive && !wasActive)
            {
                animator.SetBool("isActive" , true);
                Debug.Log("Checkpoint Activated.");
            }
            if (isVoid)
            {
                animator.SetBool("isVoid" , true);
            }
            else
            {
                animator.SetBool("isVoid" , false);
            }
            wasActive = isActive;
            wasVoid = isVoid;
        }
    }
}

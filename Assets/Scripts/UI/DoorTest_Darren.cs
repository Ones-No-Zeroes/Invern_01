using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    // Variables for switch, door control and tracking both last update, respectively
    public bool isOpen = false, isVoid = false, wasOpen = false, wasVoid = false;
    Animator animator;
    InvernLogic invernLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update isVoid property to drive switching
        invernLogic = FindFirstObjectByType<InvernLogic>();
        isVoid = invernLogic.WorldVoid;

        if (isOpen == wasOpen && isVoid == wasVoid)
        {
            return;
        }
        else
        {
            // Feed the animator the updates
            if (isVoid)
            {
                animator.SetBool("isVoid" , true);
            }
            else
            {
                animator.SetBool("isVoid" , false);
            }

            if (isOpen)
            {
                animator.SetBool("isOpen", true);
            }
            else
            {
                animator.SetBool("isOpen", false);
            }
        }

        wasOpen = isOpen;
        wasVoid = isVoid;
    }
}

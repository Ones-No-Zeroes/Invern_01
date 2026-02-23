using System.Collections;
using UnityEngine;

public class PlayerKnockbackLogic : MonoBehaviour
{
    [SerializeField] private float KnockBackTime = 0;
    [SerializeField] private Rigidbody2D rigidBody; 
    private bool isKnockBackEnabled = false;

    // Public Properties
    public bool IsKnockBackEnabled
    {
        get { return isKnockBackEnabled; }
        private set {isKnockBackEnabled = value; }
    }

    // Public Methods

    /// <summary>
    /// Methods allows for objects and NPCs to have a knockback effect on the Player. 
    /// </summary>
    /// <param name="knockBackForce"> The knockback force on the X axis</param>
    /// <param name="upwardsKnockBackForce"></param>
    public void EnableKnockBack(float directionOfKnockback, float knockBackForce = 10, float upwardsKnockBackForce = 10)
    {
        if (IsKnockBackEnabled)
        {
            return;
        }
        Debug.Log("EnableKnockBack() is ran");

        StopAllCoroutines();
        StartCoroutine(KnockbackTimer());
        
        rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
        rigidBody.AddForce(new Vector2(directionOfKnockback * knockBackForce, upwardsKnockBackForce), ForceMode2D.Impulse);
        

    }

    IEnumerator KnockbackTimer()
    {
        Debug.Log("Knockback enabled!");
        IsKnockBackEnabled = true;
        yield return new WaitForSeconds(KnockBackTime);
        IsKnockBackEnabled = false;
        Debug.Log("Knockback disabled!");
    }
}

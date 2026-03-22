using System.Collections;
using UnityEngine;

public class BossGravityFlip : GravityFlip
{   
    [Header("Boss Gravity Flip Attributes")]
    [SerializeField] private float GravitySwitchCooldownAmount;
    [SerializeField] private BossMovement bossMovement;
    [Range(0, 1)] [SerializeField] private float probabilityOfGravityFlip;

    // Private Fields
    private int randomNum; // Container for a random number;
    private bool isGravitySwitchCoolDownFinished = true;
    private int randomRangeMax = 30; // The max range used in the Random calculation
    void FixedUpdate()
    {
        if (!bossMovement.CanBossMove)
        {
            return;
        }
        
        randomNum = Random.Range(0, randomRangeMax + 1); // Grabs a random number between 0 and 30
        if (isGravitySwitchCoolDownFinished)
        {
            // If "randomNum" is greater than / equal to the randomRangeMax times probability, then gravity flip.
            // For example, if the "probabilityOfGravityFlip" is 0.5 (half/50%) and is multiplied to "randomRangeMax" (which is 30), then the result will be 15. 
            // Therefore, it will be a 50% chance of the gravity flip happening.
            if(randomNum >= randomRangeMax * probabilityOfGravityFlip) 
            {
                SwitchGravity(); 
                StartCoroutine(GravitySwitchCoolDown());
            }
        }
    }

    IEnumerator GravitySwitchCoolDown() // Coroutine for causing a cooldown between GravitySwiches
    {
        isGravitySwitchCoolDownFinished = false;
        yield return new WaitForSeconds(GravitySwitchCooldownAmount);
        isGravitySwitchCoolDownFinished = true;
    }



}

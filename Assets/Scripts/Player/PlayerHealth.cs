using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerHealth : CharacterHealth
{
    // Private Variables
    [Header("HealthBar Attributes")]
    [SerializeField] private HealthBarController healthBarController;
    [SerializeField] private CheckPointLogic checkPointLogic;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float deathDelay;
    [SerializeField] private UnityEvent OnPlayerDeathEvents;

    

    // Overridden methods

    // Overrides the DamageCharacter method, keeps the previous logic, but, calls the ApplyHealthAmountToHealthBar() method
    public override void DamageCharacter(int damageAmount)
    {
        base.DamageCharacter(damageAmount);
        healthBarController.ApplyHealthAmountToHealthBar(CurrentHealth);
    }

    // Built in Unity Methods
    protected override void Awake()
    {
        base.Awake();
        healthBarController.SetHealthBarMaxValue(MaxHealth);
        healthBarController.ApplyHealthAmountToHealthBar(CurrentHealth);
    }

 

    /// <summary>
    /// When method is called, checks to if the Player is out of bounds. VARIABLES ARE TEMP!!
    /// </summary>
    /// <param name="playerAudio"></param>
    /// <param name="death"></param>
    public void OutOfBoundsDeathChecker(AudioSource playerAudio, AudioClip death)
    {
        //Falling Out of Bounds Game Over Condition
        if (transform.position.y < -70f || transform.position.y > 140f)
        {
            KillCharacter();
            playerAudio.PlayOneShot(death);
        }

    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        healthBarController.ApplyHealthAmountToHealthBar(CurrentHealth);

        if (checkPointLogic.isCheckPointActive)
        {
            OnPlayerDeathEvents.Invoke();
            checkPointLogic.MovePlayerToCheckpoint(deathDelay);
        }
        else
        {
            OnPlayerDeathEvents.Invoke();
            StartCoroutine(DelayBeforeMainMenu());
        }
        

    }

    /// <summary>
    /// Restores the Player's health to max.
    /// </summary>
    public void RestoreHealthToMax()
    {
        CurrentHealth = MaxHealth;
        healthBarController.ApplyHealthAmountToHealthBar(MaxHealth);
        healthBarController.UnhideHealthBar();

        // Reverts the Gravity Flip
        if(gameObject.transform.localScale.y == -1)
        {
            playerController.ResetGravity();
        }
    }

    IEnumerator DelayBeforeMainMenu()
    {
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene("Menu");
    }
    
    
}

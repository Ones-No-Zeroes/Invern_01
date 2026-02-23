using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{   
    // Private Backing Fields
    [Header("Character Health Attributes")]
    [SerializeField] private int maxHealth;
    private int currentHealth;


    // Public / Protected Attributes
    protected int MaxHealth
    {
        get{return maxHealth; }
    }

    protected int CurrentHealth
    {
        get{ return currentHealth; }
        set
        {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
        }
    }

    public bool IsDead
    {
        get
        {
            if(currentHealth <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Public Methods

    /// <summary>
    /// Will damage the Character it is called on.
    /// </summary>
    /// <param name="damageAmount"></param>
    public virtual void DamageCharacter(int damageAmount)
    {
        if (IsDead)
        {
            return;
        }
        CurrentHealth -= damageAmount;
    }

    /// <summary>
    /// Currently can NOT work due to the complexity of the Player gameObject. Needed Unity things are dependent on the player being active in the scene. Must be changed.
    /// </summary>
    public virtual void KillCharacter()
    {
        CurrentHealth = 0;
    }


    // Built in Unity Methods

    protected virtual void Awake()
    {
       currentHealth = maxHealth; 
    }




}
